using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

/* 
 * DB 사용법
 * 
 * 1. 어느 폼에서든 Program.DB.openDB(프로젝트명); 을 실행하면 신규 DB 셋을 열수 있다. 이때 사용중인 DB 는 자동 종료된다.
 * 
 *    예시) Program.DB.openDB("test.sqlite");
 *    
 * 2. DB 셋이 열린 상태에서는 어느 폼에서든 Program.DB.setValue(DB유형, 테이블명, 필드리스트(,로 구분), 저장값(,로 구분), 키필드); 를 실행하여 값을 저장한다.
 *    이때 db 유형은 DB.type.ProjDB (프로젝트db), DB.type.BaseDB (기초db), DB.type.CalcDB (계산db) 이다.
 *    
 *    예시) Program.DB.setValue(DB.type.ProjDB, "연습테이블2", "연습필드3,연습필드4", "'4','3333'", "연습필드3");
 *    
 * 3. DB 셋이 열린 상태에서는 어느 폼에서든 Program.DB.getValue(DB유형, 테이블명, 필드리스트(,로 구분), 조건); 를 실행하여 값을 불러온다. 
 *    이때 값은 string[][] 의 2차원 문자열 배열로 반환된다.
 *    
 *    예시) string[][] res = Program.DB.getValue(DB.type.ProjDB, "연습테이블2", "연습필드4", "연습필드3 = '4'");
 *    
 * 4. DB 셋 중 프로젝트DB 는 1. 실행시 없으면 자동 생성된다. 이때 같이 생성되어야할 테이블들은 아래의 tables 변수의 SQL 문들이다.
 *    테이블들은 Program.DB.setValue 실행시 없으면 자동 생성된다.
 *    
 * 5. DB 셋은 프로그램 실행시 항상 오픈되어 있으므로 프로그램 실행시에는 외부 프로그램이 DB 셋에 변경값을 조회할수 없고 프로그램 종료후에 가능하다. 
 * 
 */

namespace main
{
    internal class DB
    {
        public enum type
        {
            BaseDB_HCneed,
            BaseDB_Lighting,
            BaseDB_Heating,
            BaseDB_Cooling,
            BaseDB_RESystem,
            ProjDB,
            CalcDB
        }

        private Dictionary<string, string> tables = new Dictionary<string, string>()
        {
            {"Zone", "CREATE TABLE IF NOT EXISTS Zone (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32))"},
            //{"ZoneLightgeneral", "CREATE TABLE IF NOT EXISTS ZoneLightgeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Wr VARCHAR (32),Lr VARCHAR (32),A VARCHAR (32),hR VARCHAR (32),hm VARCHAR (32),hLi VARCHAR (32),hTa VARCHAR (32),K VARCHAR (32))"},
            //{"ZoneLightprofile", "CREATE TABLE IF NOT EXISTS ZoneLightprofile (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Location VARCHAR (32),Em VARCHAR (32),KA VARCHAR (32),FA VARCHAR (32))"},
            //{"Zonedaytime", "CREATE TABLE IF NOT EXISTS Zonedaytime (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"Zonenighttime", "CREATE TABLE IF NOT EXISTS Zonenighttime (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"Lighting", "CREATE TABLE IF NOT EXISTS Lighting (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Pj VARCHAR (32),Pn VARCHAR (32),Fo VARCHAR (32),Fc VARCHAR (32),lm_W VARCHAR (32),Wsp VARCHAR (32))"},
            //{"facade1", "CREATE TABLE IF NOT EXISTS facade1 (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),direction VARCHAR (32),Aca VARCHAR (32),a VARCHAR (32),b VARCHAR (32),AD VARCHAR (32),glass VARCHAR (32),τD65_SNA VARCHAR (32),K1 VARCHAR (32),K2 VARCHAR (32),K3 VARCHAR (32),shade VARCHAR (32),dimming VARCHAR (32),γSh_lsh VARCHAR (32),γSh_hA VARCHAR (32),γSh_vA VARCHAR (32))"},
            //{"facade_shade", "CREATE TABLE IF NOT EXISTS facade_shade (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"facade_trel_D_SA", "CREATE TABLE IF NOT EXISTS facade_trel_D_SA (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"facade_trel_D_SNA", "CREATE TABLE IF NOT EXISTS facade_trel_D_SNA (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"Courtyard_Atrium", "CREATE TABLE IF NOT EXISTS Courtyard_Atrium (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),aIn_At VARCHAR (32),bIn_At VARCHAR (32),hIn_At VARCHAR (32),glasstype VARCHAR (32),τSh_In_At_D65 VARCHAR (32),Ksh_In_At_1 VARCHAR (32),Ksh_In_At_2 VARCHAR (32),Ksh_In_At_3 VARCHAR (32))"},
            //{"Doubleskin", "CREATE TABLE IF NOT EXISTS Doubleskin (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),glasstype VARCHAR (32),τSh_In_GDF_D65 VARCHAR (32),Ksh_GDF_1 VARCHAR (32),Ksh_GDF_2 VARCHAR (32),Ksh_GDF_3 VARCHAR (32))"},
            //{"NaturalLighting", "CREATE TABLE IF NOT EXISTS NaturalLighting (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Main VARCHAR (32),Middle VARCHAR (32),Sub VARCHAR (32))"},
            //{"rooflight1", "CREATE TABLE IF NOT EXISTS rooflight1 (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),direction VARCHAR (32),Aca VARCHAR (32),a VARCHAR (32),b VARCHAR (32),AD VARCHAR (32),glasstype VARCHAR (32),γF VARCHAR (32),γW VARCHAR (32),a_s VARCHAR (32),b_s VARCHAR (32),hS VARCHAR (32),hw VARCHAR (32),hg VARCHAR (32),Da VARCHAR (32),τD65_SNA VARCHAR (32),τD65_SA VARCHAR (32),Kobl_1 VARCHAR (32),Kobl_2 VARCHAR (32),Kobl_3 VARCHAR (32),shading VARCHAR (32),dimmingtype VARCHAR (32))"},
            //{"rooflight_shade", "CREATE TABLE IF NOT EXISTS rooflight_shade (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"renewable_energy_1", "CREATE TABLE IF NOT EXISTS renewable_energy_1 (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),energytype VARCHAR (32),direction VARCHAR (32),inc VARCHAR (32),area VARCHAR (32),eff VARCHAR (32))"},
            //{"ext_ill", "CREATE TABLE IF NOT EXISTS ext_ill (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"User_Lighting", "CREATE TABLE IF NOT EXISTS User_Lighting (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),등기구명칭 VARCHAR (32),램프유형 VARCHAR (32),제조사 VARCHAR (32),안정기_컨버터 VARCHAR (32),광속 VARCHAR (32),소비전력 VARCHAR (32),광효율 VARCHAR (32),조명계수 VARCHAR (32))"},
            //{"User_Renew", "CREATE TABLE IF NOT EXISTS User_Renew (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),집광채광명칭 VARCHAR (32),집광채광종류 VARCHAR (32),제조사 VARCHAR (32),집광채광효율 VARCHAR (32),산광부가로길이 VARCHAR (32),산광부세로길이 VARCHAR (32),산광부면적 VARCHAR (32))"},
            //{"ZoneLighting_form", "CREATE TABLE IF NOT EXISTS ZoneLighting_form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),조명방식 VARCHAR (32),제어방식 VARCHAR (32),디밍유형 VARCHAR (32),조명개수 VARCHAR (32),조명밀도 VARCHAR (32),재실계수 VARCHAR (32),재실계수1 VARCHAR (32),재실계수2 VARCHAR (32),재실계수3 VARCHAR (32),조도제어계수 VARCHAR (32),자연채광체크 VARCHAR (32),자연채광유형 VARCHAR (32),파사드 VARCHAR (32),이중외피유리 VARCHAR (32),아트리움유리 VARCHAR (32),파사드유리빛투과율 VARCHAR (32),파사드너비 VARCHAR (32),파사드길이 VARCHAR (32),파사드높이 VARCHAR (32),천창 VARCHAR (32),천창유리각 VARCHAR (32),천창수평측면각 VARCHAR (32),천창장변부길이 VARCHAR (32),천창단변부길이 VARCHAR (32),천창수평상부높이 VARCHAR (32),차양 VARCHAR (32),집광채광체크  VARCHAR (32),집광채광번호 VARCHAR (32),집광채광명칭 VARCHAR (32),집광채광종류 VARCHAR (32),집광채광효율 VARCHAR (32),집광채광면적 VARCHAR (32),표준길이1 VARCHAR (32),표준길이2 VARCHAR (32),표준너비 VARCHAR (32),사용자길이1 VARCHAR (32),사용자길이2 VARCHAR (32),사용자면적 VARCHAR (32), 조명번호 VARCHAR (32), 등기구명칭 VARCHAR (32), 램프유형 VARCHAR (32), 컨버터_안정기 VARCHAR (32), 광속 VARCHAR (32), 소비전력 VARCHAR (32), 조명계수 VARCHAR (32),표준광속 VARCHAR (32), 표준소비전력 VARCHAR (32),사용자광속 VARCHAR (32), 사용자소비전력 VARCHAR (32),사용자예상전력 VARCHAR (32))"},
            //{"Zone_LightResult", "CREATE TABLE IF NOT EXISTS Zone_LightResult (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32), 월 VARCHAR (32),ITr VARCHAR (32),IRD VARCHAR (32),ISh_Ish VARCHAR (32),ISh_hA VARCHAR (32),Ish_vA VARCHAR (32),Ish_In_At VARCHAR (32),Wi VARCHAR (32),Ish_GDF VARCHAR (32),Ish VARCHAR (32),f_τeff_SNA VARCHAR (32),f_D VARCHAR (32),f_nearD VARCHAR (32),f_DCA VARCHAR (32),f_dclass VARCHAR (32),f_nearEm_SNA VARCHAR (32),f_fd_sna VARCHAR (32),f_fd_sa VARCHAR (32),f_nearEm_DC VARCHAR (32),f_fd_c VARCHAR (32),f_FDS VARCHAR (32),f_FD VARCHAR (32),as_bs VARCHAR (32),hs_bs VARCHAR (32),hg_hw VARCHAR (32),normal_ηR VARCHAR (32),saw_ηR VARCHAR (32),r_DSNA VARCHAR (32),r_DSA VARCHAR (32),r_dclass VARCHAR (32),r_nearEm_FDS VARCHAR (32),r_fd_sna VARCHAR (32),r_fd_sa VARCHAR (32),r_nearEm_DC VARCHAR (32),r_fd_c VARCHAR (32),r_FDS VARCHAR (32),r_FD VARCHAR (32),Sunlight_SCW VARCHAR (32),Sunlight_PjSC VARCHAR (32),Final_W VARCHAR (32))"},



             {"Zone_HCneed", "CREATE TABLE IF NOT EXISTS Zone_HCneed (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32), 이름 VARCHAR (32), 난방_냉방 VARCHAR (32), 비이용일_이용일 VARCHAR (32), 월 VARCHAR (32), HT_tot VARCHAR (32), HT_Wall VARCHAR (32), HT_Roof VARCHAR (32), HT_Floor VARCHAR (32), HT_GWall VARCHAR (32), HT_Door VARCHAR (32), HT_Win VARCHAR (32), HT_CW VARCHAR (32), HT_Di_Wall VARCHAR (32), HT_Indi_Wall VARCHAR (32), HT_Di_Roof VARCHAR (32), HT_Indi_Roof VARCHAR (32), HT_Di_Win VARCHAR (32), HT_Indi_Win VARCHAR (32), HT_Di_Door VARCHAR (32), HT_Indi_Door VARCHAR (32), HT_TB_tot VARCHAR (32), HT_TB_Wall VARCHAR (32), HT_TB_Roof VARCHAR (32), HT_TB_Floor VARCHAR (32), HT_TB_Gwall VARCHAR (32), HT_TB_Win VARCHAR (32), HT_TB_Door VARCHAR (32), HT_TB_CW VARCHAR (32), HV_tot VARCHAR (32), HV_inf VARCHAR (32), HV_win VARCHAR (32), HV_z VARCHAR (32), HV_mech VARCHAR (32), H_tot VARCHAR (32), tao VARCHAR (32), dwe_mth VARCHAR (32), dwd_mth VARCHAR (32), theta_i VARCHAR (32), theta_e VARCHAR (32), QTsink_tot VARCHAR (32), QTsink_Wall VARCHAR (32), QTsink_Roof VARCHAR (32), QTsink_Floor VARCHAR (32), QTsink_GWall VARCHAR (32), QTsink_Door VARCHAR (32), QTsink_Win VARCHAR (32), QTsink_CW VARCHAR (32), QTsource_tot VARCHAR (32), QTsource_Wall VARCHAR (32), QTsource_Roof VARCHAR (32), QTsource_Floor VARCHAR (32), QTsource_GWall VARCHAR (32), QTsource_Door VARCHAR (32), QTsource_Win VARCHAR (32), QTsource_CW VARCHAR (32), QSopsink_tot VARCHAR (32), QSopsource_tot VARCHAR (32), QStr_tot VARCHAR (32), QSopsink_Wall VARCHAR (32), QSopsink_Roof VARCHAR (32), QSopsink_Door VARCHAR (32), QSopsink_CW_p VARCHAR (32), QSopsource_Wall VARCHAR (32), QSopsource_Roof VARCHAR (32), QSopsource_Door VARCHAR (32), QSopsource_CW_p VARCHAR (32), QStr_Win VARCHAR (32), QStr_CW VARCHAR (32), QVsink_tot VARCHAR (32), QV_inf_sink VARCHAR (32), QV_win_sink VARCHAR (32), QV_z_sink VARCHAR (32), QV_mech_sink VARCHAR (32), QVsource_tot VARCHAR (32), QV_inf_source VARCHAR (32), QV_win_source VARCHAR (32), QV_z_source VARCHAR (32), QV_mech_source VARCHAR (32), QI_tot VARCHAR (32), QI_L VARCHAR (32), QI_P VARCHAR (32), QI_fac VARCHAR (32), Qsink VARCHAR (32), Qsource VARCHAR (32), gamma VARCHAR (32), a VARCHAR (32), eta VARCHAR (32), dQc_b VARCHAR (32), dQc_sink VARCHAR (32), Qhb_we_day VARCHAR (32), Qhb_wd_day VARCHAR (32), Qcb_we_day VARCHAR (32), Qcb_wd_day VARCHAR (32), Qhb_mth VARCHAR (32), Qcb_mth VARCHAR (32), Qhb_we_mth VARCHAR (32), Qhb_wd_mth VARCHAR (32), Qcb_we_mth VARCHAR (32), Qcb_wd_mth VARCHAR (32), Qhb_a VARCHAR (32),  Qcb_a VARCHAR (32),  Qhb_we_a VARCHAR (32),  Qhb_wd_a VARCHAR (32),  Qcb_we_a VARCHAR (32),  Qcb_wd_a VARCHAR (32))"},
            //{"OutairTemperature", "CREATE TABLE IF NOT EXISTS OutairTemperature (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (8), 온도 REAL,일 INTEGER)"},
            //{"Zonegeneral", "CREATE TABLE IF NOT EXISTS Zonegeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT, 구분 VARCHAR (8),zoneNum VARCHAR (32),zoneName VARCHAR (32),zoneUsage VARCHAR (32),zoneHC VARCHAR (32),θi_h_set VARCHAR (32),θi_c_set VARCHAR (32),Δθi_NA,Fx VARCHAR (32),Fx_fl VARCHAR (32),Fx_wl VARCHAR (32),θs_c VARCHAR (32),θi_h_min VARCHAR (32),θe_min VARCHAR (32),θSUP_Wi VARCHAR (32),Mode_night VARCHAR (32),Mode_we VARCHAR (32),twd_d VARCHAR (32),th_op_d_we VARCHAR (32),th_op_d VARCHAR (32),dwd_a VARCHAR (32),ZoneArea VARCHAR (32),zoneHeight VARCHAR (32),qI_p VARCHAR (32),qI_fac VARCHAR (32),Cwirk_A VARCHAR (32),VA_we VARCHAR (32),VA_wd VARCHAR (32),n50 VARCHAR (32),e VARCHAR (32),f VARCHAR (32),Vmech_SUP_we VARCHAR (32),Vmech_SUP_wd VARCHAR (32),Vmech_ETA_we VARCHAR (32),Vmech_ETA_wd VARCHAR (32),ηV_mech VARCHAR (32),ηχV_mech VARCHAR (32),χi_c_set VARCHAR (32),χi_h_set VARCHAR (32),Vmech_SUP_z VARCHAR (32),Vmech_ETA_z VARCHAR (32),ρacp_a VARCHAR (32))"},
            //{"ZoneWall", "CREATE TABLE IF NOT EXISTS ZoneWall (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),α VARCHAR (32),Degree VARCHAR (32))"},
            //{"ZoneRoof", "CREATE TABLE IF NOT EXISTS ZoneRoof (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),α VARCHAR (32),Degree VARCHAR (32))"},
            //{"ZoneFloor", "CREATE TABLE IF NOT EXISTS ZoneFloor (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32))"},
            //{"ZoneGWall", "CREATE TABLE IF NOT EXISTS ZoneGWall (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32))"},
            //{"ZoneDoor", "CREATE TABLE IF NOT EXISTS ZoneDoor (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),α VARCHAR (32),Degree VARCHAR (32))"},
            //{"ZoneWin", "CREATE TABLE IF NOT EXISTS ZoneWin (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Uvalue VARCHAR (32),Uinst VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),Ff VARCHAR (32),g VARCHAR (32),τ VARCHAR (32),gtot VARCHAR (32),τtot VARCHAR (32),degree VARCHAR (32))"},
            //{"ZoneCW", "CREATE TABLE IF NOT EXISTS ZoneCW (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area_g VARCHAR (32),Uvalue_g VARCHAR (32),Ff_g VARCHAR (32),g_g VARCHAR (32),gtot_g VARCHAR (32),τ_g VARCHAR (32),τtot_g VARCHAR (32),Area_p VARCHAR (32),Uvalue_p VARCHAR (32),α_p VARCHAR (32),Area_d VARCHAR (32),Uvalue_d VARCHAR (32),Ff_d VARCHAR (32),g_d VARCHAR (32),τ_d VARCHAR (32),Area_tot VARCHAR (32),Uinst VARCHAR (32))"},
            //{"ZoneWall_Solar", "CREATE TABLE IF NOT EXISTS ZoneWall_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneRoof_Solar", "CREATE TABLE IF NOT EXISTS ZoneRoof_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneDoor_Solar", "CREATE TABLE IF NOT EXISTS ZoneDoor_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneCW_Solar", "CREATE TABLE IF NOT EXISTS ZoneCW_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneWin_Solar", "CREATE TABLE IF NOT EXISTS ZoneWin_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneWin_Shadow", "CREATE TABLE IF NOT EXISTS ZoneWin_Shadow (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneWin_a", "CREATE TABLE IF NOT EXISTS ZoneWin_a (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneCW_shadow", "CREATE TABLE IF NOT EXISTS ZoneCW_shadow (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            //{"ZoneCW_a", "CREATE TABLE IF NOT EXISTS ZoneCW_a (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},

            {"BuildingGeneral", "CREATE TABLE IF NOT EXISTS BuildingGeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트명 VARCHAR (32),프로젝트유형 VARCHAR (32),사업성능목표 VARCHAR (32),건물진단실시 VARCHAR (32),건물대상 VARCHAR (32),건물용도 VARCHAR (32),건물명 VARCHAR (32),주소 VARCHAR (32),지역인덱스 VARCHAR (32),지역 VARCHAR (32),지역구분 VARCHAR (32),외벽구조유형 VARCHAR (32),지붕구조유형 VARCHAR (32),준공연도 VARCHAR (32),준공월 VARCHAR (32),준공시기 VARCHAR (32),법규시기 VARCHAR (32),연면적 VARCHAR (32),건축면적 VARCHAR (32),지상층수 VARCHAR (32),지하층수 VARCHAR (32),작성자 VARCHAR (32),작성자주소 VARCHAR (32),작성자회사 VARCHAR (32),작성연도 VARCHAR (32),작성월 VARCHAR (32),작성시기 VARCHAR (32))"},
            {"User_Material", "CREATE TABLE IF NOT EXISTS User_Material (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),구분 VARCHAR (32),재료명 VARCHAR (32),종류2 VARCHAR (32),종류1 VARCHAR (32),열전도율 VARCHAR (32),밀도 VARCHAR (32),투습저항계수dry VARCHAR (32),투습저항계수wet VARCHAR (32),비열 VARCHAR (32),비고 VARCHAR (32))"},
            {"ConstructionWall", "CREATE TABLE IF NOT EXISTS ConstructionWall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존외벽 VARCHAR (32),덧댐커튼월 VARCHAR (32),U적용방법 VARCHAR (32),직접간접 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교종류 VARCHAR (32),외장재색 VARCHAR (32),표면열전달저항기준 VARCHAR (32),선형점형 VARCHAR (32),A VARCHAR (32),B VARCHAR (32),C VARCHAR (32),PsiKai VARCHAR (32),단위면적당적용 VARCHAR (32),Rse VARCHAR (32),Rsi VARCHAR (32),두께합계 VARCHAR (32),열저항합계 VARCHAR (32),단열재두께 VARCHAR (32),재료1종류 VARCHAR (32),재료1두께 VARCHAR (32),재료2종류 VARCHAR (32),재료2두께 VARCHAR (32),재료3종류 VARCHAR (32),재료3두께 VARCHAR (32),재료4종류 VARCHAR (32),재료4두께 VARCHAR (32),재료5종류 VARCHAR (32),재료5두께 VARCHAR (32),재료6종류 VARCHAR (32),재료6두께 VARCHAR (32),재료7종류 VARCHAR (32),재료7두께 VARCHAR (32),재료8종류 VARCHAR (32),재료8두께 VARCHAR (32),재료9종류 VARCHAR (32),재료9두께 VARCHAR (32),재료10종류 VARCHAR (32),재료10두께 VARCHAR (32),흡수율 VARCHAR (32),열관류율 VARCHAR (32),열교가산치 VARCHAR (32),유효열관류율 VARCHAR (32))"},
            {"ConstructionCW", "CREATE TABLE IF NOT EXISTS ConstructionCW (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존커튼월 VARCHAR (32),Ucw적용방법 VARCHAR (32),직접간접 VARCHAR (32),프레임유형 VARCHAR (32),프레임종류 VARCHAR (32),고정유리종류 VARCHAR (32),개폐유리종류 VARCHAR (32),간봉종류 VARCHAR (32),설치유형 VARCHAR (32),설치종류 VARCHAR (32),LE_CL_V VARCHAR (32),패널적용유무 VARCHAR (32),패널종류 VARCHAR (32),패널유리종류 VARCHAR (32),LE_CL_V_Panel VARCHAR (32),출입문적용유무 VARCHAR (32),출입문프레임유형 VARCHAR (32),출입문프레임종류 VARCHAR (32),출입문유리종류 VARCHAR (32),출입문간봉종류 VARCHAR (32),LE_CL_V_Door VARCHAR (32),고정유리열관류율 VARCHAR (32),개폐유리열관류율 VARCHAR (32),태양열취득률 VARCHAR (32),빛투과율 VARCHAR (32),고정유리선형열관류율 VARCHAR (32),개폐유리선형열관류율 VARCHAR (32),고정프레임열관류율 VARCHAR (32),개폐프레임열관류율 VARCHAR (32),고정프레임두께 VARCHAR (32),개폐프레임두께 VARCHAR (32),패널열관류율 VARCHAR (32),패널유리열관류율 VARCHAR (32),패널열전도율 VARCHAR (32),패널흡수율 VARCHAR (32),패널선형열관류율 VARCHAR (32),패널두께 VARCHAR (32),출입문유리열관류율 VARCHAR (32),출입문태양열취득률 VARCHAR (32),출입문빛투과율 VARCHAR (32),출입문유리선형열관류율 VARCHAR (32),출입문프레임두께 VARCHAR (32),출입문프레임열관류율 VARCHAR (32),상부설치열관류율 VARCHAR (32),측면설치열관류율 VARCHAR (32),하부설치열관류율 VARCHAR (32),사이즈명칭 VARCHAR (32),커튼월면적 VARCHAR (32),너비 VARCHAR (32),높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32),패널면적 VARCHAR (32),패널둘레길이 VARCHAR (32),M_T프레임면적 VARCHAR (32),개폐창프레임면적 VARCHAR (32),출입문프레임면적 VARCHAR (32),출입문유리면적 VARCHAR (32),출입문유리둘레길이 VARCHAR (32),커튼월창열관류율 VARCHAR (32),유리부분열관류율 VARCHAR (32),패널부분열관류율 VARCHAR (32),출입문부분열관류율 VARCHAR (32),설치열교가산치 VARCHAR (32),커튼월창유효열관류율 VARCHAR (32),유리부분유효열관류율 VARCHAR (32),패널부분유효열관류율 VARCHAR (32),출입문부분유효열관류율 VARCHAR (32),유리부분유리면적비 VARCHAR (32),출입문부분유리면적비 VARCHAR (32))"},
            {"ConstructionWindow", "CREATE TABLE IF NOT EXISTS ConstructionWindow (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),창호명칭 VARCHAR (32),Type VARCHAR (32),기존창호 VARCHAR (32),Uw적용방법 VARCHAR (32),직접간접 VARCHAR (32),프레임유형 VARCHAR (32),이중단창 VARCHAR (32),프레임재료 VARCHAR (32),프레임종류 VARCHAR (32),유리종류 VARCHAR (32),간봉종류 VARCHAR (32),설치유형 VARCHAR (32),설치종류 VARCHAR (32),LE_CL_V VARCHAR (32),유리열관류율 VARCHAR (32),태양열취득률 VARCHAR (32),빛투과율 VARCHAR (32),고정유리선형열관류율 VARCHAR (32),개폐유리선형열관류율 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),중간바프레임열관류율 VARCHAR (32),개폐부프레임두께 VARCHAR (32),고정부프레임두께 VARCHAR (32),중간바프레임두께 VARCHAR (32),상부설치열관류율 VARCHAR (32),측면설치열관류율 VARCHAR (32),하부설치열관류율 VARCHAR (32),창호열관류율 VARCHAR (32))"},
            {"ConstructionFloor", "CREATE TABLE IF NOT EXISTS ConstructionFloor (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존바닥 VARCHAR (32),기초설치 VARCHAR (32),U적용방법 VARCHAR (32),직접간접 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교종류 VARCHAR (32),표면열전달저항기준 VARCHAR (32),선형점형 VARCHAR (32),A VARCHAR (32),B VARCHAR (32),C VARCHAR (32),PsiKai VARCHAR (32),단위면적당적용 VARCHAR (32),Rse VARCHAR (32),Rsi VARCHAR (32),두께합계 VARCHAR (32),열저항합계 VARCHAR (32),단열재두께 VARCHAR (32),재료1종류 VARCHAR (32),재료1두께 VARCHAR (32),재료2종류 VARCHAR (32),재료2두께 VARCHAR (32),재료3종류 VARCHAR (32),재료3두께 VARCHAR (32),재료4종류 VARCHAR (32),재료4두께 VARCHAR (32),재료5종류 VARCHAR (32),재료5두께 VARCHAR (32),재료6종류 VARCHAR (32),재료6두께 VARCHAR (32),재료7종류 VARCHAR (32),재료7두께 VARCHAR (32),재료8종류 VARCHAR (32),재료8두께 VARCHAR (32),재료9종류 VARCHAR (32),재료9두께 VARCHAR (32),재료10종류 VARCHAR (32),재료10두께 VARCHAR (32),열관류율 VARCHAR (32),열교가산치 VARCHAR (32),유효열관류율 VARCHAR (32))"},
            {"ConstructionRoof", "CREATE TABLE IF NOT EXISTS ConstructionRoof (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존지붕 VARCHAR (32),U적용방법 VARCHAR (32),직접간접 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교종류 VARCHAR (32),외장재색 VARCHAR (32),표면열전달저항기준 VARCHAR (32),선형점형 VARCHAR (32),A VARCHAR (32),B VARCHAR (32),C VARCHAR (32),PsiKai VARCHAR (32),단위면적당적용 VARCHAR (32),Rse VARCHAR (32),Rsi VARCHAR (32),두께합계 VARCHAR (32),열저항합계 VARCHAR (32),단열재두께 VARCHAR (32),재료1종류 VARCHAR (32),재료1두께 VARCHAR (32),재료2종류 VARCHAR (32),재료2두께 VARCHAR (32),재료3종류 VARCHAR (32),재료3두께 VARCHAR (32),재료4종류 VARCHAR (32),재료4두께 VARCHAR (32),재료5종류 VARCHAR (32),재료5두께 VARCHAR (32),재료6종류 VARCHAR (32),재료6두께 VARCHAR (32),재료7종류 VARCHAR (32),재료7두께 VARCHAR (32),재료8종류 VARCHAR (32),재료8두께 VARCHAR (32),재료9종류 VARCHAR (32),재료9두께 VARCHAR (32),재료10종류 VARCHAR (32),재료10두께 VARCHAR (32),흡수율 VARCHAR (32),열관류율 VARCHAR (32),열교가산치 VARCHAR (32),유효열관류율 VARCHAR (32))"},
            {"SubWindow", "CREATE TABLE IF NOT EXISTS SubWindow (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),상위창호번호 VARCHAR (32),창호면적 VARCHAR (32),창호너비 VARCHAR (32),창호높이 VARCHAR (32),고정유리면적 VARCHAR (32),개폐유리면적 VARCHAR (32),개폐프레임면적 VARCHAR (32),고정프레임면적 VARCHAR (32),중간프레임면적 VARCHAR (32),고정유리둘레길이 VARCHAR (32),개폐유리둘레길이 VARCHAR (32),창호열관류율 VARCHAR (32),설치열교가산치 VARCHAR (32),창호유효열관류율 VARCHAR (32),유리면적비 VARCHAR (32))"},
            {"SubCW", "CREATE TABLE IF NOT EXISTS SubCW (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),)"},
            {"Import_WindowSize", "CREATE TABLE IF NOT EXISTS Import_WindowSize (ID INTEGER PRIMARY KEY AUTOINCREMENT,창호명칭 VARCHAR (32),창호면적 VARCHAR (32),창호너비 VARCHAR (32),창호높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),개폐프레임면적 VARCHAR (32),고정프레임면적 VARCHAR (32),중간프레임면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32))"},
            {"Import_CWSize", "CREATE TABLE IF NOT EXISTS Import_CWSize (ID INTEGER PRIMARY KEY AUTOINCREMENT,명칭 VARCHAR (32),커튼월면적 VARCHAR (32),너비 VARCHAR (32),높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32),패널면적 VARCHAR (32),패널둘레길이 VARCHAR (32),M_T프레임면적 VARCHAR (32),개폐창프레임면적 VARCHAR (32),출입문프레임면적 VARCHAR (32),출입문유리면적 VARCHAR (32),출입문유리둘레길이 VARCHAR (32))"},
            {"User_WindowFrame", "CREATE TABLE User_WindowFrame (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),프레임종류 VARCHAR (32),프레임재료 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),중간바프레임열관류율 VARCHAR (32),개폐부프레임두께 VARCHAR (32),고정부프레임두께 VARCHAR (32),중간바프레임두께 VARCHAR (32),시험성적서이미지 VARCHAR (32))"},
            {"User_CWFrame", "CREATE TABLE User_CWFrame (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),패널엣지선형열관류율 VARCHAR (32),M_T프레임두께 VARCHAR (32),fr프레임두께 VARCHAR (32),시험성적서이미지 VARCHAR (32))"},
            {"User_Glass", "CREATE TABLE User_Glass (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),복층_삼중_단창 VARCHAR (32),아르곤_공기 VARCHAR (32),LE_CL_V VARCHAR (32),열관류율 VARCHAR (32),태양열취득율 VARCHAR (32),빛투과율 VARCHAR (32),외부반사율 VARCHAR (32),내부반사율 VARCHAR (32))"},
            {"User_DoubleGlass", "CREATE TABLE User_DoubleGlass (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),복층_삼중_단창 VARCHAR (32),아르곤_공기 VARCHAR (32),LE_CL_V VARCHAR (32),열관류율 VARCHAR (32),태양열취득율 VARCHAR (32),빛투과율 VARCHAR (32),외부반사율 VARCHAR (32),내부반사율 VARCHAR (32))"},
            {"User_WindowSpacer", "CREATE TABLE User_WindowSpacer (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),구분3 VARCHAR (32),고정유리_CL_선형열관류율 VARCHAR (32),개폐유리_CL_선형열관류율 VARCHAR (32),고정유리_LE_선형열관류율 VARCHAR (32),개폐유리_LE_선형열관류율 VARCHAR (32),LE_CL_V VARCHAR (32))"},
            {"User_CWSpacer", "CREATE TABLE User_CWSpacer (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분1 VARCHAR (32),구분3 VARCHAR (32),고정유리_CL_선형열관류율 VARCHAR (32),개폐유리_CL_선형열관류율 VARCHAR (32),고정유리_LE_선형열관류율 VARCHAR (32),개폐유리_LE_선형열관류율 VARCHAR (32),LE_CL_V VARCHAR (32))"},
            {"User_CWDoorFrame", "CREATE TABLE IF NOT EXISTS User_CWDoorFrame (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분 VARCHAR (32),프레임열관류율 VARCHAR (32),프레임두께 VARCHAR (32))"},
            {"User_WindowInstall", "CREATE TABLE User_WindowInstall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),구분3 VARCHAR (32),구분4 VARCHAR (32),상부설치선형열관류율 VARCHAR (32),측면설치선형열관류율 VARCHAR (32),하부설치선형열관류율 VARCHAR (32))"},
            {"User_CWInstall", "CREATE TABLE User_CWInstall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),구분3 VARCHAR (32),상부설치선형열관류율 VARCHAR (32),측면설치선형열관류율 VARCHAR (32),하부설치선형열관류율 VARCHAR (32))"},
            {"ZoneGeneral_Form", "CREATE TABLE IF NOT EXISTS ZoneGeneral_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),존이름 VARCHAR (32),실제어방식 VARCHAR (32),냉난방유무 VARCHAR (32),환기유무 VARCHAR (32),환기방식 VARCHAR (32),온도교환효율 VARCHAR (32),전열교환효율 VARCHAR (32),용도프로필 VARCHAR (32),순바닥면적 VARCHAR (32),천장고 VARCHAR (32),시작시간 VARCHAR (32),종료시간 VARCHAR (32),주이용일 VARCHAR (32),재실자수 VARCHAR (32),기기발열수준 VARCHAR (32),일일급탕요구량 VARCHAR (32),냉난방시간 VARCHAR (32),사용시간 VARCHAR (32),공조시간 VARCHAR (32),연이용일수 VARCHAR (32),재실밀도 VARCHAR (32),재실수준 VARCHAR (32),일일인체발열 VARCHAR (32),면적당인체발열 VARCHAR (32),일일기기발열 VARCHAR (32),면적당기기발열 VARCHAR (32),순체적 VARCHAR (32),환기횟수 VARCHAR (32),이용일환기량 VARCHAR (32),비이용일환기량  VARCHAR (32),천장축열선택 VARCHAR (32),외벽축열선택 VARCHAR (32),내벽축열선택 VARCHAR (32),바닥축열선택 VARCHAR (32),천장축열 VARCHAR (32),외벽축열 VARCHAR (32),내벽축열 VARCHAR (32),바닥축열 VARCHAR (32),천장면적 VARCHAR (32),외벽면적 VARCHAR (32),내벽면적 VARCHAR (32),바닥면적 VARCHAR (32),존축열성능 VARCHAR  VARCHAR (32),존기밀타입 VARCHAR (32),기밀적용유형 VARCHAR (32),q50 VARCHAR (32),n50 VARCHAR (32))"},
            {"Zonegeneral_3D", "CREATE TABLE IF NOT EXISTS ZoneGeneral_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),층 VARCHAR (32),지면접합유형 VARCHAR (32),바닥면적 VARCHAR (32),주향 VARCHAR (32),주광너비 VARCHAR (32),주광깊이 VARCHAR (32),상인방높이 VARCHAR (32))"},
            {"ZoneEnvelope_3D", "CREATE TABLE IF NOT EXISTS ZoneEnvelope_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,아이디 VARCHAR (32),번호 VARCHAR (32),층 VARCHAR (32),존 VARCHAR (32),외피유형 VARCHAR (32),커튼월부위 VARCHAR (32),면적 VARCHAR (32),인접존 VARCHAR (32),방위 VARCHAR (32),기울기 VARCHAR (32),우측면돌출각도 VARCHAR (32),좌측면돌출각도 VARCHAR (32),상부돌출각도 VARCHAR (32),주변요소음영각도 VARCHAR (32),구조체 VARCHAR (32),구조체번호 VARCHAR (32),우측면돌출길이 VARCHAR (32),좌측면돌출길이 VARCHAR (32),상부돌출길이 VARCHAR (32),주변요소음영길이 VARCHAR (32),벽체길이 VARCHAR (32))"},
            {"ThermalBridge_3D", "CREATE TABLE IF NOT EXISTS ThermalBridge_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,열교항목 VARCHAR (32),열교길이 VARCHAR (32))"},


            {"HeatingSystem_Form", "CREATE TABLE IF NOT EXISTS HeatingSystem_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32))"},
            {"ZoneSystem_Form", "CREATE TABLE IF NOT EXISTS ZoneSystem_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),난방시스템 VARCHAR (32))"},
            {"User_Boiler", "CREATE TABLE IF NOT EXISTS User_Boiler (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),난방급탕 VARCHAR (32),연료 VARCHAR (32),Type VARCHAR (32),용량 VARCHAR (32),전부하효율 VARCHAR (32),부분부하효율 VARCHAR (32),소비전력 VARCHAR (32),대기전력 VARCHAR (32))"},
        };
   

        private SQLiteConnection? baseDB_hcneed, baseDB_lighting, baseDB_heating, baseDB_cooling, baseDB_resystem, projDB, calcDB;
        public bool openDB(string projPath)
        {
            closeDB();

            SQLiteCommand cmd = new SQLiteCommand();
            
            //요구량 baseDB
            if (GetFileSize("basedb_hcneed.sqlite") > 0)
            {
                baseDB_hcneed = new SQLiteConnection(@"Data Source=basedb_hcneed.sqlite");
                baseDB_hcneed.Open();

                if (baseDB_hcneed.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB_hcneed;
                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();
            }
            else
            {
                return false;
            }

            //조명 baseDB
            if (GetFileSize("basedb_lighting.sqlite") > 0)
            {
                baseDB_lighting = new SQLiteConnection(@"Data Source=basedb_lighting.sqlite");
                baseDB_lighting.Open();

                if (baseDB_lighting.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB_lighting;
                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();
            }
            else
            {
                return false;
            }
            //난방 baseDB
            if (GetFileSize("basedb_heating.sqlite") > 0)
            {
                baseDB_heating = new SQLiteConnection(@"Data Source=basedb_heating.sqlite");
                baseDB_heating.Open();

                if (baseDB_heating.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB_heating;
                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();
            }
            else
            {
                return false;
            }
            //냉방 baseDB
            if (GetFileSize("basedb_cooling.sqlite") > 0)
            {
                baseDB_cooling = new SQLiteConnection(@"Data Source=basedb_cooling.sqlite");
                baseDB_cooling.Open();

                if (baseDB_cooling.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB_cooling;
                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();
            }
            else
            {
                return false;
            }

            //신재생 baseDB
            if (GetFileSize("basedb_resystem.sqlite") > 0)
            {
                baseDB_resystem = new SQLiteConnection(@"Data Source=basedb_resystem.sqlite");
                baseDB_resystem.Open();

                if (baseDB_resystem.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB_resystem;
                cmd.CommandText = "PRAGMA synchronous=OFF";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "PRAGMA journal_mode=OFF";
                cmd.ExecuteNonQuery();
            }
            else
            {
                return false;
            }
            if (GetFileSize(projPath) <= 0)
            {
                File.Copy("templ.sqlite", projPath, true);
            }

            projDB = new SQLiteConnection(@"Data Source=" + projPath);
            projDB.Open();

            if (projDB.State != ConnectionState.Open)
            {
                baseDB_hcneed.Close();
                baseDB_hcneed.Dispose();

                baseDB_lighting.Close();
                baseDB_lighting.Dispose();

                baseDB_heating.Close();
                baseDB_heating.Dispose();

                baseDB_cooling.Close();
                baseDB_cooling.Dispose();

                baseDB_resystem.Close();
                baseDB_resystem.Dispose();

                return false;
            }

            cmd.Connection = projDB;
            cmd.CommandText = "PRAGMA synchronous=OFF";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "PRAGMA journal_mode=OFF";
            cmd.ExecuteNonQuery();

            calcDB = new SQLiteConnection(@"Data Source=:memory:");
            calcDB.Open();
            if (calcDB.State != ConnectionState.Open)
            {
                baseDB_hcneed.Close();
                baseDB_hcneed.Dispose();

                baseDB_lighting.Close();
                baseDB_lighting.Dispose();

                baseDB_heating.Close();
                baseDB_heating.Dispose();

                baseDB_cooling.Close();
                baseDB_cooling.Dispose();


                baseDB_resystem.Close();
                baseDB_resystem.Dispose();

                projDB.Close();
                projDB.Dispose();
                return false;
            }

            return true;
        }
        public void closeDB()
        {
            if (baseDB_hcneed != null)
            {
                baseDB_hcneed.Close();
                baseDB_hcneed.Dispose();
            }
            if (baseDB_lighting != null)
            {
                baseDB_lighting.Close();
                baseDB_lighting.Dispose();
            }
            if (baseDB_heating != null)
            {
                baseDB_heating.Close();
                baseDB_heating.Dispose();
            }
            if (baseDB_cooling != null)
            {
                baseDB_cooling.Close();
                baseDB_cooling.Dispose();
            }
            if (baseDB_resystem != null)
            {
                baseDB_resystem.Close();
                baseDB_resystem.Dispose();
            }
            if (projDB != null)
            {
                projDB.Close();
                projDB.Dispose();
            }

            if (calcDB != null)
            {
                calcDB.Close();
                calcDB.Dispose();
            }
        }

        public void initTable (type dbType, string table)
        {
            try
            {
                createTable(dbType, table, tables[table]);
            }
            catch (Exception e)
            {

            }
        }

        public void initTables (type dbType)
        {
            try
            {
                foreach (var table in tables)
                {
                    createTable(dbType, table.Key, table.Value);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void executeSQL(type dbType, string exec)
        {
            if (exec != "")
            {
                switch (dbType)
                {
                    case type.BaseDB_HCneed:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB_hcneed);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.BaseDB_Lighting:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB_lighting);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.BaseDB_Heating:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB_heating);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.BaseDB_Cooling:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB_cooling);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.BaseDB_RESystem:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB_resystem);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.ProjDB:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, projDB);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case type.CalcDB:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, calcDB);
                            cmd.ExecuteNonQuery();
                        }
                        break;
                }
            }
        }
        public string[][] querySQL(type dbType, string query)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            List<string[]> objects = new List<string[]>();

            if (query != "")
            {
                switch (dbType)
                {
                    case type.BaseDB_HCneed:
                        cmd.Connection = baseDB_hcneed;
                        break;
                    case type.BaseDB_Lighting:
                        cmd.Connection = baseDB_lighting;
                        break;
                    case type.BaseDB_Heating:
                        cmd.Connection = baseDB_heating;
                        break;
                    case type.BaseDB_Cooling:
                        cmd.Connection = baseDB_cooling;
                        break;
                    case type.BaseDB_RESystem:
                        cmd.Connection = baseDB_resystem;
                        break;
                    case type.ProjDB:
                        cmd.Connection = projDB;
                        break;
                    case type.CalcDB:
                        cmd.Connection = calcDB;
                        break;
                }

                cmd.CommandText = query;

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    string json = string.Empty;

                    while (reader.Read())
                    {
                        string[] rec = new string[reader.FieldCount];

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            rec[i] = reader[i].ToString();
                        }
                        objects.Add(rec);
                    }
                }
            }
            return objects.ToArray();
        }
        private long GetFileSize(string filePath)
        {
            long fileSize = 0;
            if (File.Exists(filePath))
            {
                FileInfo info = new FileInfo(filePath);
                fileSize = info.Length;
            }

            return fileSize;
        }
        public void createTable(type dbType, string name, string exec)
        {
            try
            {
                if (exec != "")
                {
                    SQLiteCommand? cmd = null;

                    switch (dbType)
                    {
                        case type.ProjDB:
                            cmd = new SQLiteCommand(projDB);
                            break;
                        case type.CalcDB:
                            cmd = new SQLiteCommand(calcDB);
                            break;
                    }

                    if (cmd != null)
                    {
                        bool found = false;
                        cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='" + name + "';";
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            cmd.CommandText = exec;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void setValue(type dbType, string table, string columns, string values, string key_columns)
        {
            try
            {
                createTable(dbType, table, tables[table]);

                string[] cols = columns.Split(',');
                string[] vals = values.Split(',');
                string[] keys = key_columns.Split(',');

                SQLiteCommand cmd = new SQLiteCommand();

                Program.UTIL.trim(cols);
                Program.UTIL.trim(vals);
                Program.UTIL.trim(keys);

                switch (dbType)
                {
                    case type.ProjDB:
                        cmd.Connection = projDB;
                        break;
                    case type.CalcDB:
                        cmd.Connection = calcDB;
                        break;
                }

                string condition = "";

                {
                    int i = -1;
                    string cond = "";

                    while (++i < keys.Length)
                    {
                        int n = Array.FindIndex(cols, el => el == keys[i]);

                        if (n >= 0)
                        {
                            if (cond != "")
                            {
                                cond += " AND ";
                            }
                            cond += cols[n] + " = " + vals[n];
                        }
                    }

                    if (cond != "")
                    {
                        cmd.CommandText = "SELECT * FROM " + table + " WHERE " + cond;
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read() && reader.HasRows)
                            {
                                condition = cond;
                            }
                        }
                    }
                }

                if (condition == "")
                {
                    cmd.CommandText = "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ")";
                }
                else
                {
                    int i = -1;
                    string upd = "";

                    cmd.CommandText = "UPDATE " + table + " SET ";

                    while (++i < cols.Length)
                    {
                        if (upd != "") upd += ",";
                        upd += cols[i] + "=" + vals[i];
                    }

                    cmd.CommandText += upd + " WHERE " + condition;
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void deleteTable(type dbType, string table)
        {
            try
            {

                SQLiteCommand cmd = new SQLiteCommand();


                switch (dbType)
                {
                    case type.ProjDB:
                        cmd.Connection = projDB;
                        break;
                    case type.CalcDB:
                        cmd.Connection = calcDB;
                        break;
                }

                string condition = "";

                    cmd.CommandText = "delete from " + table;


                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }


        public bool CopyValue(type dbType, string table, string conditions = "",string Num="")
        {

            if (conditions != "")
            {
                string[][] res = querySQL(dbType, "PRAGMA table_info(" + table + ")");


                if (res.Length > 0)
                {
                    int i = 0;
                    string columns = "";


                    while (++i < res.Length)
                    {
                        if (columns != "") columns += ",";
                        columns += res[i][1];
                    }

                    if (columns != "")
                    {
                        try
                        {
                            SQLiteCommand cmd = new SQLiteCommand();

                            switch (dbType)
                            {
                                case type.ProjDB:
                                    cmd.Connection = projDB;
                                    break;
                                case type.CalcDB:
                                    cmd.Connection = calcDB;
                                    break;
                            }

                            cmd.CommandText = "INSERT INTO " + table + " (" + columns + ") SELECT " + columns + " FROM " + table + " WHERE " + conditions + " LIMIT 1";

                            cmd.ExecuteNonQuery();
                            string[][] res1 = Program.DB.querySQL(DB.type.ProjDB, "SELECT MAX(ID) AS id FROM "+ table);
                            Program.DB.executeSQL(DB.type.ProjDB, "UPDATE "+ table + " SET 번호='" + Num + "' WHERE  ID = " + res1[0][0]);
                        }
                        catch (Exception ex)
                        {
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        public void deleteValue(type dbType, string table, string conditions = "")
        {
            try
            {

                SQLiteCommand cmd = new SQLiteCommand();


                switch (dbType)
                {
                    case type.ProjDB:
                        cmd.Connection = projDB;
                        break;
                    case type.CalcDB:
                        cmd.Connection = calcDB;
                        break;
                }

                string condition = "";

                if (conditions != "")
                {
                    cmd.CommandText = "delete from " + table + " WHERE " + conditions;
                }
                else
                {
                    
                }


                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        public string[][] getValue(type dbType, string table, string columns, string conditions = "")
        {
            SQLiteCommand cmd = new SQLiteCommand();
            List<string[]> objects = new List<string[]>();

            switch (dbType)
            {
                case type.BaseDB_HCneed:
                    cmd.Connection = baseDB_hcneed;
                    break;
                case type.BaseDB_Lighting:
                    cmd.Connection = baseDB_lighting;
                    break;
                case type.BaseDB_Heating:
                    cmd.Connection = baseDB_heating;
                    break;
                case type.BaseDB_Cooling:
                    cmd.Connection = baseDB_cooling;
                    break;
                case type.BaseDB_RESystem:
                    cmd.Connection = baseDB_resystem;
                    break;
                case type.ProjDB:
                    cmd.Connection = projDB;
                    break;
                case type.CalcDB:
                    cmd.Connection = calcDB;
                    break;
            }

            if (conditions != "")
            {
                cmd.CommandText = "SELECT " + columns + " FROM " + table + " WHERE " + conditions;
            }
            else
            {
                cmd.CommandText = "SELECT " + columns + " FROM " + table;
            }

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                string json = string.Empty;

                while (reader.Read())
                {
                    string[] rec = new string[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rec[i] = reader[i].ToString();
                    }
                    objects.Add(rec);
                }
            }

            return objects.ToArray();
        }

        //중복 제거하고 값 가져오기
        public string[][] getValue_dedupe(type dbType, string table, string columns, string conditions = "")
        {
            SQLiteCommand cmd = new SQLiteCommand();
            List<string[]> objects = new List<string[]>();

            switch (dbType)
            {
                case type.BaseDB_HCneed:
                    cmd.Connection = baseDB_hcneed;
                    break;
                case type.BaseDB_Lighting:
                    cmd.Connection = baseDB_lighting;
                    break;
                case type.BaseDB_Heating:
                    cmd.Connection = baseDB_heating;
                    break;
                case type.BaseDB_Cooling:
                    cmd.Connection = baseDB_cooling;
                    break;
                case type.BaseDB_RESystem:
                    cmd.Connection = baseDB_resystem;
                    break;
                case type.ProjDB:
                    cmd.Connection = projDB;
                    break;
                case type.CalcDB:
                    cmd.Connection = calcDB;
                    break;
            }

            if (conditions != "")
            {
                cmd.CommandText = "SELECT DISTINCT " + columns + " FROM " + table + " WHERE " + conditions;
            }
            else
            {
                cmd.CommandText = "SELECT DISTINCT " + columns + " FROM " + table  ;
            }

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                string json = string.Empty;

                while (reader.Read())
                {
                    string[] rec = new string[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rec[i] = reader[i].ToString();
                    }
                    objects.Add(rec);
                }
            }

            return objects.ToArray();
        }
    }
}



