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
            BaseDB,
            ProjDB,
            CalcDB
        }

        private Dictionary<string, string> tables = new Dictionary<string, string>()
        {
            {"Zone", "CREATE TABLE IF NOT EXISTS Zone (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32))"},
            {"ZoneLightgeneral", "CREATE TABLE IF NOT EXISTS ZoneLightgeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Wr VARCHAR (32),Lr VARCHAR (32),A VARCHAR (32),hR VARCHAR (32),hm VARCHAR (32),hLi VARCHAR (32),hTa VARCHAR (32),K VARCHAR (32))"},
            {"ZoneLightprofile", "CREATE TABLE IF NOT EXISTS ZoneLightprofile (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Location VARCHAR (32),Em VARCHAR (32),KA VARCHAR (32),FA VARCHAR (32))"},
            {"Zonedaytime", "CREATE TABLE IF NOT EXISTS Zonedaytime (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"Zonenighttime", "CREATE TABLE IF NOT EXISTS Zonenighttime (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"Lighting", "CREATE TABLE IF NOT EXISTS Lighting (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Pj VARCHAR (32),Pn VARCHAR (32),Fo VARCHAR (32),Fc VARCHAR (32),lm_W VARCHAR (32),Wsp VARCHAR (32))"},
            {"facade1", "CREATE TABLE IF NOT EXISTS facade1 (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),direction VARCHAR (32),Aca VARCHAR (32),a VARCHAR (32),b VARCHAR (32),AD VARCHAR (32),glass VARCHAR (32),τD65_SNA VARCHAR (32),K1 VARCHAR (32),K2 VARCHAR (32),K3 VARCHAR (32),shade VARCHAR (32),dimming VARCHAR (32),γSh_lsh VARCHAR (32),γSh_hA VARCHAR (32),γSh_vA VARCHAR (32))"},
            {"facade_shade", "CREATE TABLE IF NOT EXISTS facade_shade (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"facade_trel_D_SA", "CREATE TABLE IF NOT EXISTS facade_trel_D_SA (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"facade_trel_D_SNA", "CREATE TABLE IF NOT EXISTS facade_trel_D_SNA (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"Courtyard_Atrium", "CREATE TABLE IF NOT EXISTS Courtyard_Atrium (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),aIn_At VARCHAR (32),bIn_At VARCHAR (32),hIn_At VARCHAR (32),glasstype VARCHAR (32),τSh_In_At_D65 VARCHAR (32),Ksh_In_At_1 VARCHAR (32),Ksh_In_At_2 VARCHAR (32),Ksh_In_At_3 VARCHAR (32))"},
            {"Doubleskin", "CREATE TABLE IF NOT EXISTS Doubleskin (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),glasstype VARCHAR (32),τSh_In_GDF_D65 VARCHAR (32),Ksh_GDF_1 VARCHAR (32),Ksh_GDF_2 VARCHAR (32),Ksh_GDF_3 VARCHAR (32))"},
            {"NaturalLighting", "CREATE TABLE IF NOT EXISTS NaturalLighting (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),Main VARCHAR (32),Middle VARCHAR (32),Sub VARCHAR (32))"},
            {"rooflight1", "CREATE TABLE IF NOT EXISTS rooflight1 (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),direction VARCHAR (32),Aca VARCHAR (32),a VARCHAR (32),b VARCHAR (32),AD VARCHAR (32),glasstype VARCHAR (32),γF VARCHAR (32),γW VARCHAR (32),a_s VARCHAR (32),b_s VARCHAR (32),hS VARCHAR (32),hw VARCHAR (32),hg VARCHAR (32),Da VARCHAR (32),τD65_SNA VARCHAR (32),τD65_SA VARCHAR (32),Kobl_1 VARCHAR (32),Kobl_2 VARCHAR (32),Kobl_3 VARCHAR (32),shading VARCHAR (32),dimmingtype VARCHAR (32))"},
            {"rooflight_shade", "CREATE TABLE IF NOT EXISTS rooflight_shade (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"renewable_energy_1", "CREATE TABLE IF NOT EXISTS renewable_energy_1 (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),energytype VARCHAR (32),direction VARCHAR (32),inc VARCHAR (32),area VARCHAR (32),eff VARCHAR (32))"},
            {"ext_ill", "CREATE TABLE IF NOT EXISTS ext_ill (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},

            {"OutairTemperature", "CREATE TABLE IF NOT EXISTS OutairTemperature (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32),월 VARCHAR (8), 온도 REAL,일 INTEGER)"},
            {"Zonegeneral", "CREATE TABLE IF NOT EXISTS Zonegeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT, 구분 VARCHAR (8),zoneNum VARCHAR (32),zoneName VARCHAR (32),zoneUsage VARCHAR (32),zoneHC VARCHAR (32),θi_h_set VARCHAR (32),θi_c_set VARCHAR (32),Δθi_NA,Fx VARCHAR (32),Fx_fl VARCHAR (32),Fx_wl VARCHAR (32),θs_c VARCHAR (32),θi_h_min VARCHAR (32),θe_min VARCHAR (32),θSUP_Wi VARCHAR (32),Mode_night VARCHAR (32),Mode_we VARCHAR (32),twd_d VARCHAR (32),th_op_d_we VARCHAR (32),th_op_d VARCHAR (32),dwd_a VARCHAR (32),ZoneArea VARCHAR (32),zoneHeight VARCHAR (32),qI_p VARCHAR (32),qI_fac VARCHAR (32),Cwirk_A VARCHAR (32),VA_we VARCHAR (32),VA_wd VARCHAR (32),n50 VARCHAR (32),e VARCHAR (32),f VARCHAR (32),Vmech_SUP_we VARCHAR (32),Vmech_SUP_wd VARCHAR (32),Vmech_ETA_we VARCHAR (32),Vmech_ETA_wd VARCHAR (32),ηV_mech VARCHAR (32),ηχV_mech VARCHAR (32),χi_c_set VARCHAR (32),χi_h_set VARCHAR (32),Vmech_SUP_z VARCHAR (32),Vmech_ETA_z VARCHAR (32),ρacp_a VARCHAR (32))"},
            {"ZoneWall", "CREATE TABLE IF NOT EXISTS ZoneWall (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),α VARCHAR (32),Degree VARCHAR (32))"},
            {"ZoneRoof", "CREATE TABLE IF NOT EXISTS ZoneRoof (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),α VARCHAR (32),Degree VARCHAR (32))"},
            {"ZoneFloor", "CREATE TABLE IF NOT EXISTS ZoneFloor (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32))"},
            {"ZoneGWall", "CREATE TABLE IF NOT EXISTS ZoneGWall (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32))"},
            {"ZoneDoor", "CREATE TABLE IF NOT EXISTS ZoneDoor (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Ueff VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),α VARCHAR (32),Degree VARCHAR (32))"},
            {"ZoneWin", "CREATE TABLE IF NOT EXISTS ZoneWin (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area VARCHAR (32),Uvalue VARCHAR (32),Uinst VARCHAR (32),DirectInDirect VARCHAR (32),Direction VARCHAR (32),Ff VARCHAR (32),g VARCHAR (32),τ VARCHAR (32),gtot VARCHAR (32),τtot VARCHAR (32),degree VARCHAR (32))"},
            {"ZoneCW", "CREATE TABLE IF NOT EXISTS ZoneCW (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), Name VARCHAR (32),Area_g VARCHAR (32),Uvalue_g VARCHAR (32),Ff_g VARCHAR (32),g_g VARCHAR (32),gtot_g VARCHAR (32),τ_g VARCHAR (32),τtot_g VARCHAR (32),Area_p VARCHAR (32),Uvalue_p VARCHAR (32),α_p VARCHAR (32),Area_d VARCHAR (32),Uvalue_d VARCHAR (32),Ff_d VARCHAR (32),g_d VARCHAR (32),τ_d VARCHAR (32),Area_tot VARCHAR (32),Uinst VARCHAR (32))"},
            {"ZoneWall_Solar", "CREATE TABLE IF NOT EXISTS ZoneWall_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneRoof_Solar", "CREATE TABLE IF NOT EXISTS ZoneRoof_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneDoor_Solar", "CREATE TABLE IF NOT EXISTS ZoneDoor_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneCW_Solar", "CREATE TABLE IF NOT EXISTS ZoneCW_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneWin_Solar", "CREATE TABLE IF NOT EXISTS ZoneWin_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneWin_Shadow", "CREATE TABLE IF NOT EXISTS ZoneWin_Shadow (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneWin_a", "CREATE TABLE IF NOT EXISTS ZoneWin_a (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},
            {"ZoneCW_shadow", "CREATE TABLE IF NOT EXISTS ZoneCW_shadow (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"},

            {"User_Material", "CREATE TABLE IF NOT EXISTS User_Material (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),구분 VARCHAR (32),재료명 VARCHAR (32),종류2 VARCHAR (32),종류1 VARCHAR (32),열전도율 VARCHAR (32),밀도 VARCHAR (32),투습저항계수dry VARCHAR (32),투습저항계수wet VARCHAR (32),비열 VARCHAR (32),비고 VARCHAR (32))"},
            {"ConstructionCW", "CREATE TABLE IF NOT EXISTS ConstructionCW (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존커튼월 VARCHAR (32),Ucw적용방법 VARCHAR (32),직접간접 VARCHAR (32),프레임유형 VARCHAR (32),프레임종류 VARCHAR (32),고정유리종류 VARCHAR (32),개폐유리종류 VARCHAR (32),간봉종류 VARCHAR (32),설치유형 VARCHAR (32),설치종류 VARCHAR (32),LE_CL_V VARCHAR (32),패널적용유무 VARCHAR (32),패널종류 VARCHAR (32),패널유리종류 VARCHAR (32),LE_CL_V_Panel VARCHAR (32),출입문적용유무 VARCHAR (32),출입문프레임유형 VARCHAR (32),출입문프레임종류 VARCHAR (32),출입문유리종류 VARCHAR (32),출입문간봉종류 VARCHAR (32),LE_CL_V_Door VARCHAR (32),고정유리열관류율 VARCHAR (32),개폐유리열관류율 VARCHAR (32),태양열취득률 VARCHAR (32),빛투과율 VARCHAR (32),고정유리선형열관류율 VARCHAR (32),개폐유리선형열관류율 VARCHAR (32),고정프레임열관류율 VARCHAR (32),개폐프레임열관류율 VARCHAR (32),고정프레임두께 VARCHAR (32),개폐프레임두께 VARCHAR (32),패널열관류율 VARCHAR (32),패널유리열관류율 VARCHAR (32),패널열전도율 VARCHAR (32),패널흡수율 VARCHAR (32),패널선형열관류율 VARCHAR (32),패널두께 VARCHAR (32),출입문유리열관류율 VARCHAR (32),출입문태양열취득률 VARCHAR (32),출입문빛투과율 VARCHAR (32),출입문유리선형열관류율 VARCHAR (32),출입문프레임두께 VARCHAR (32),출입문프레임열관류율 VARCHAR (32),상부설치열관류율 VARCHAR (32),측면설치열관류율 VARCHAR (32),하부설치열관류율 VARCHAR (32),사이즈명칭 VARCHAR (32),커튼월면적 VARCHAR (32),너비 VARCHAR (32),높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32),패널면적 VARCHAR (32),패널둘레길이 VARCHAR (32),M_T프레임면적 VARCHAR (32),개폐창프레임면적 VARCHAR (32),출입문프레임면적 VARCHAR (32),출입문유리면적 VARCHAR (32),출입문유리둘레길이 VARCHAR (32),커튼월창열관류율 VARCHAR (32),유리부분열관류율 VARCHAR (32),패널부분열관류율 VARCHAR (32),출입문부분열관류율 VARCHAR (32),설치열교가산치 VARCHAR (32),커튼월창유효열관류율 VARCHAR (32),유리부분유효열관류율 VARCHAR (32),패널부분유효열관류율 VARCHAR (32),출입문부분유효열관류율 VARCHAR (32))"},
            {"ConstructionWindow", "CREATE TABLE IF NOT EXISTS ConstructionWindow (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),창호명칭 VARCHAR (32),Type VARCHAR (32),기존창호 VARCHAR (32),Uw적용방법 VARCHAR (32),직접간접 VARCHAR (32),프레임유형 VARCHAR (32),이중단창 VARCHAR (32),프레임재료 VARCHAR (32),프레임종류 VARCHAR (32),유리종류 VARCHAR (32),간봉종류 VARCHAR (32),설치유형 VARCHAR (32),설치종류 VARCHAR (32),LE_CL_V VARCHAR (32),유리열관류율 VARCHAR (32),태양열취득률 VARCHAR (32),빛투과율 VARCHAR (32),고정유리선형열관류율 VARCHAR (32),개폐유리선형열관류율 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),중간바프레임열관류율 VARCHAR (32),개폐부프레임두께 VARCHAR (32),고정부프레임두께 VARCHAR (32),중간바프레임두께 VARCHAR (32),상부설치열관류율 VARCHAR (32),측면설치열관류율 VARCHAR (32),하부설치열관류율 VARCHAR (32),창호열관류율 VARCHAR (32))"},
            {"SubWindow", "CREATE TABLE IF NOT EXISTS SubWindow (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),상위창호번호 VARCHAR (32),창호면적 VARCHAR (32),창호너비 VARCHAR (32),창호높이 VARCHAR (32),고정유리면적 VARCHAR (32),개폐유리면적 VARCHAR (32),개폐프레임면적 VARCHAR (32),고정프레임면적 VARCHAR (32),중간프레임면적 VARCHAR (32),고정유리둘레길이 VARCHAR (32),개폐유리둘레길이 VARCHAR (32),창호열관류율 VARCHAR (32),설치열교가산치 VARCHAR (32),창호유효열관류율 VARCHAR (32))"},
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
            {"ZoneGeneral", "CREATE TABLE IF NOT EXISTS ZoneGeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),존이름 VARCHAR (32),층 VARCHAR (32),길이 VARCHAR (32),깊이 VARCHAR (32),바닥면적 VARCHAR (32),용도프로필 VARCHAR (32),천장고 VARCHAR (32),시작시간 VARCHAR (32),종료시간 VARCHAR (32),주이용일 VARCHAR (32),재실자수 VARCHAR (32),기기발열수준 VARCHAR (32),일일급탕요구량 VARCHAR (32),냉난방시간 VARCHAR (32),사용시간 VARCHAR (32),공조시간 VARCHAR (32),연이용일수 VARCHAR (32),재실밀도 VARCHAR (32),재실수준 VARCHAR (32),일일인체발열 VARCHAR (32),면적당인체발열 VARCHAR (32),일일기기발열 VARCHAR (32),면적당기기발열 VARCHAR (32),순체적 VARCHAR (32),환기횟수 VARCHAR (32),환기량 VARCHAR (32))"},
            {"ZoneEnvelope", "CREATE TABLE IF NOT EXISTS ZoneEnvelope (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),기호 VARCHAR (32),층 VARCHAR (32),존 VARCHAR (32),외피유형 VARCHAR (32),커튼월부위 VARCHAR (32),면적 VARCHAR (32),인접존 VARCHAR (32),방위 VARCHAR (32),기울기 VARCHAR (32),우측면돌출 VARCHAR (32),좌측면돌출 VARCHAR (32),상부돌출 VARCHAR (32),주변요소 VARCHAR (32),구조체 VARCHAR (32),Ueff VARCHAR (32),α VARCHAR (32),g VARCHAR (32),직접간접 VARCHAR (32))"},
            {"ZoneCW_a", "CREATE TABLE IF NOT EXISTS ZoneCW_a (ID INTEGER PRIMARY KEY AUTOINCREMENT,zoneNum VARCHAR (32), 구조체 VARCHAR (32),월 VARCHAR (32),value VARCHAR (32))"}
        };

        private SQLiteConnection? baseDB, projDB, calcDB;
        public bool openDB(string projPath)
        {
            closeDB();

            SQLiteCommand cmd = new SQLiteCommand();

            if (GetFileSize("basedb.sqlite") > 0)
            {
                baseDB = new SQLiteConnection(@"Data Source=basedb.sqlite");
                baseDB.Open();

                if (baseDB.State != ConnectionState.Open)
                {
                    return false;
                }

                cmd.Connection = baseDB;
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
                baseDB.Close();
                baseDB.Dispose();

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
                baseDB.Close();
                baseDB.Dispose();
                projDB.Close();
                projDB.Dispose();
                return false;
            }

            return true;
        }
        public void closeDB()
        {
            if (baseDB != null)
            {
                baseDB.Close();
                baseDB.Dispose();
            }

            if(projDB != null)
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
            catch (Exception e) { }
        }

        public void executeSQL(type dbType, string exec)
        {
            if (exec != "")
            {
                switch (dbType)
                {
                    case type.BaseDB:
                        {
                            SQLiteCommand cmd = new SQLiteCommand(exec, baseDB);
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
                    case type.BaseDB:
                        cmd.Connection = baseDB;
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
                            string[][] res1 = Program.DB.querySQL(DB.type.ProjDB, "SELECT MAX(ID) AS id FROM ConstructionWindow");

                            Program.DB.executeSQL(DB.type.ProjDB, "UPDATE ConstructionWindow SET 번호='" + Num + "' WHERE  ID = " + res1[0][0]);
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
                case type.BaseDB:
                    cmd.Connection = baseDB;
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
    }
}



