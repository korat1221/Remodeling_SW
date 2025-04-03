using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
    public class SecureSQLite
    {
        [DllImport("wrap_sqlite.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static public extern void SetInfo(string path, bool isEncrypt);
        [DllImport("wrap_sqlite.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static public extern int OpenDB(string path, int idx);
        [DllImport("wrap_sqlite.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static public extern int OpenMemoryDB(int idx);
        [DllImport("wrap_sqlite.dll")]
        static public extern int CloseDB(int idx);
        [DllImport("wrap_sqlite.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static public extern int SaveDB(string path, int idx);
        [DllImport("wrap_sqlite.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static public extern IntPtr QuerySQL(int idx, string sql);
        [DllImport("wrap_sqlite.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        static public extern int ExecuteSQL(int idx, string sql);
    }

    internal class DB
    {
        public enum type
        {
            BaseDB_HCneed = 0,
            BaseDB_Lighting,
            BaseDB_Heating,
            BaseDB_Cooling,
            BaseDB_AHU,
            BaseDB_RESystem,
            BaseDB_Optimal,
            ProjDB,
            CalcDB,
            ProjListDB

        }

        //    private string PASSWORD = "abcd";
        private const int customDB = 12;
        private bool useCaches = false;
        private string projDBPath = "";
        private Dictionary<type, Dictionary<string, string[][]>> caches = new Dictionary<type, Dictionary<string, string[][]>>();
        private Dictionary<string, Dictionary<string, string[][]>> caches2 = new Dictionary<string, Dictionary<string, string[][]>>();
        private Dictionary<string, string> tables = new Dictionary<string, string>()
        {
            //프로젝트유형 기존:1, 리트로핏:2, 리모델링:3, 신규:4
            //Building 
            {"BuildingGeneral", "CREATE TABLE IF NOT EXISTS BuildingGeneral (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트명 VARCHAR (32),프로젝트유형 VARCHAR (32),프로젝트유형번호 VARCHAR (32),기존프로젝트 VARCHAR (32),사업성능목표 VARCHAR (32),건물진단실시 VARCHAR (32),건물대상 VARCHAR (32),건물용도 VARCHAR (32),건물명 VARCHAR (32),주소 VARCHAR (32),지역인덱스 VARCHAR (32),지역 VARCHAR (32),지역구분 VARCHAR (32),외벽구조유형 VARCHAR (32),지붕구조유형 VARCHAR (32),준공연도 VARCHAR (32),준공월 VARCHAR (32),준공시기 VARCHAR (32),법규시기 VARCHAR (32),연면적 VARCHAR (32),건축면적 VARCHAR (32),지상층수 VARCHAR (32),지하층수 VARCHAR (32),작성자 VARCHAR (32),작성자주소 VARCHAR (32),작성자회사 VARCHAR (32),작성연도 VARCHAR (32),작성월 VARCHAR (32),작성시기 VARCHAR (32),기밀측정여부 VARCHAR (32),출입문기밀여부 VARCHAR (32),창호기밀여부 VARCHAR (32),배선기밀여부 VARCHAR (32),배관기밀여부 VARCHAR (32),n50 VARCHAR (32),출입문q50 VARCHAR (32),창호q50 VARCHAR (32),외벽q50 VARCHAR (32),지붕q50 VARCHAR (32),외벽dUtb VARCHAR (32),지붕dUtb VARCHAR (32),바닥dUtb VARCHAR (32))"},
            {"BlowDoorTest", "CREATE TABLE IF NOT EXISTS BlowDoorTest (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),측정위치 VARCHAR (32),CMH VARCHAR (32),ACH VARCHAR (32),Volume VARCHAR (32))"},
            {"BuildingEnergyUse", "CREATE TABLE IF NOT EXISTS BuildingEnergyUse (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트유형 VARCHAR (32),연료 VARCHAR (32),연도 VARCHAR (32),월 VARCHAR (32),단위 VARCHAR (32),에너지사용량 VARCHAR (32),사용시작일 VARCHAR (32),사용종료일 VARCHAR (32))"}, 
            {"User_PV", "CREATE TABLE IF NOT EXISTS User_PV (ID INTEGER PRIMARY KEY AUTOINCREMENT, 번호 VARCHAR (32), 프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),CELLTYPE VARCHAR (32),길이 VARCHAR (32),높이 VARCHAR (32),정격출력 VARCHAR (32),Kpk VARCHAR (32),설치 VARCHAR (32))"},
            {"User_PVInverter", "CREATE TABLE IF NOT EXISTS User_PVInverter (ID INTEGER PRIMARY KEY AUTOINCREMENT, 번호 VARCHAR (32), 프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),효율 VARCHAR (32))"},
            {"User_PVBattery", "CREATE TABLE IF NOT EXISTS User_PVBattery (ID INTEGER PRIMARY KEY AUTOINCREMENT, 번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),정격전력 VARCHAR (32),배터리타입 VARCHAR (32))"},
            {"User_FC", "CREATE TABLE IF NOT EXISTS User_FC (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),연료 VARCHAR (32),전기출력 VARCHAR (32),전기효율 VARCHAR (32),열출력 VARCHAR (32),열효율 VARCHAR (32),대수 VARCHAR (32),설치 VARCHAR (32))"},
            {"User_WP", "CREATE TABLE IF NOT EXISTS User_WP (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),타입 VARCHAR (32),세부타입 VARCHAR (32),정격출력 VARCHAR (32),회전면적 VARCHAR (32),허브높이 VARCHAR (32),시동풍속 VARCHAR (32),최적풍속 VARCHAR (32),종단풍속 VARCHAR (32),시동풍속전력계수 VARCHAR (32),최적풍속전력계수 VARCHAR (32),종단풍속전력계수 VARCHAR (32),신규기존 VARCHAR (32))"},
            {"User_WPInverter", "CREATE TABLE IF NOT EXISTS User_WPInverter (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32), 프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),EURO효율 VARCHAR (32))"},
            {"User_Boiler", "CREATE TABLE IF NOT EXISTS User_Boiler (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),난방급탕 VARCHAR (32),연료 VARCHAR (32),Type VARCHAR (32),용량 VARCHAR (32),전부하효율 VARCHAR (32),부분부하효율 VARCHAR (32),소비전력 VARCHAR (32),대기전력 VARCHAR (32),대수 VARCHAR (32),신규기존 VARCHAR (32))"},
            {"User_AirHP", "CREATE TABLE IF NOT EXISTS User_AirHP (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),난방냉방 VARCHAR (32),연료 VARCHAR (32),공급유형 VARCHAR (32),냉방정격용량 VARCHAR (32),냉방정격COP VARCHAR (32),냉방정격소비전력 VARCHAR (32),난방정격용량 VARCHAR (32),난방정격COP VARCHAR (32),난방정격소비전력 VARCHAR (32),한랭지용량 VARCHAR (32),한랭지COP VARCHAR (32),한랭지소비전력 VARCHAR (32),대기전력 VARCHAR (32),대수 VARCHAR (32),설치 VARCHAR (32))"},
            {"User_GroundHP", "CREATE TABLE IF NOT EXISTS User_GroundHP (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),난방냉방 VARCHAR, 연료 VARCHAR (32),공급유형 VARCHAR (32),수직수평 VARCHAR (32),냉방용량 VARCHAR (32),냉방EER VARCHAR (32),냉방소비전력 VARCHAR (32),난방정격용량 VARCHAR (32),난방정격COP VARCHAR (32),난방정격소비전력 VARCHAR (32),난방등급2용량 VARCHAR (32),난방등급2COP VARCHAR (32),난방등급2소비전력 VARCHAR (32),대수 VARCHAR (32),냉수입구온도 VARCHAR (32),냉수출구온도 VARCHAR (32),압축기 VARCHAR (32),증발기 VARCHAR (32),설치 VARCHAR (32), 대기전력 VARCHAR(32))"},
            {"User_GroundWHP", "CREATE TABLE IF NOT EXISTS User_GroundWHP (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),난방냉방 VARCHAR (32), 연료 VARCHAR (32),공급유형 VARCHAR (32),수직수평 VARCHAR (32),냉방용량 VARCHAR (32),냉방EER VARCHAR (32),냉방소비전력 VARCHAR (32),난방정격용량 VARCHAR (32),난방정격COP VARCHAR (32),난방정격소비전력 VARCHAR (32),난방등급2용량 VARCHAR (32),난방등급2COP VARCHAR (32),난방등급2소비전력 VARCHAR (32),대수 VARCHAR (32),냉수입구온도 VARCHAR (32),냉수출구온도 VARCHAR (32),압축기 VARCHAR (32),증발기 VARCHAR (32),설치 VARCHAR (32), 대기전력 VARCHAR(32))"},
            {"User_Pump", "CREATE TABLE IF NOT EXISTS User_Pump (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),종류 VARCHAR (32),A효율 VARCHAR (32),B효율 VARCHAR (32),유량 VARCHAR (32),동력 VARCHAR (32),양정 VARCHAR (32),대수 VARCHAR (32),신규기존 VARCHAR (32))"},
            {"User_ce", "CREATE TABLE IF NOT EXISTS User_ce (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),난방냉방 VARCHAR (32),종류 VARCHAR (32),용량_냉방 VARCHAR (32),소비전력_냉방 VARCHAR (32),용량_난방 VARCHAR (32),소비전력_난방 VARCHAR (32),온도제어방식 VARCHAR (32),대수 VARCHAR (32),신규기존 VARCHAR (32))"},
            {"User_Solar", "CREATE TABLE IF NOT EXISTS User_Solar (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),난방급탕 VARCHAR (32),모듈면적 VARCHAR (32),효율 VARCHAR (32),열손실계수1차 VARCHAR (32),열손실계수2차 VARCHAR (32),입사각50도 VARCHAR (32),유효열용량 VARCHAR (32),신규기존 VARCHAR (32))"},
            {"User_ABS", "CREATE TABLE IF NOT EXISTS User_ABS (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32), 난방냉방 VARCHAR (32),연료 VARCHAR (32),지역난방 VARCHAR (32),냉방용량 VARCHAR (32),냉방성능 VARCHAR (32),난방용량 VARCHAR (32),난방성능 VARCHAR (32),냉수입구온도 VARCHAR (32),냉수출구온도 VARCHAR (32),온수입구온도 VARCHAR (32),온수출구온도 VARCHAR (32),대기전력 VARCHAR (32),통합성능 VARCHAR (32),대수 VARCHAR (32),설치 VARCHAR (32))"},
            {"User_DH", "CREATE TABLE IF NOT EXISTS User_DH (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),용도 VARCHAR (32),용량 VARCHAR (32),공급온도1차 VARCHAR (32),환수온도1차 VARCHAR (32),공급온도2차 VARCHAR (32),환수온도2차 VARCHAR (32),대수 VARCHAR (32),신규기존 VARCHAR (32))"},
            {"User_AHU", "CREATE TABLE IF NOT EXISTS User_AHU (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),설치유형 VARCHAR (32),공조방식 VARCHAR (32),열회수유형 VARCHAR (32),온도교환효율_냉방 VARCHAR (32),온도교환효율_난방 VARCHAR (32),전열교환효율_냉방 VARCHAR (32),전열교환효율_난방 VARCHAR (32),습도교환효율_냉방 VARCHAR (32),습도교환효율_난방 VARCHAR (32),냉각코일출력 VARCHAR (32),냉각코일_입구_건구온도 VARCHAR (32),냉각코일_입구_습구온도 VARCHAR (32),냉각코일_출구_건구온도 VARCHAR (32),냉각코일_출구_습구온도 VARCHAR (32),난방코일출력 VARCHAR (32),난방코일_입구온도 VARCHAR (32),난방코일_출구온도 VARCHAR (32),가습기유형 VARCHAR (32),가습기제어유형 VARCHAR (32),가습기습도수준 VARCHAR (32),가습기용량 VARCHAR (32),급기풍량 VARCHAR (32),배기풍량 VARCHAR (32),급기정압 VARCHAR (32),배기정압 VARCHAR (32),급기팬동력 VARCHAR (32),배기팬동력 VARCHAR (32),모터제어 VARCHAR (32))"},
            {"User_HRV", "CREATE TABLE IF NOT EXISTS User_HRV (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),설치유형 VARCHAR (32),열회수유형 VARCHAR (32),온도교환효율_냉방 VARCHAR (32),온도교환효율_난방 VARCHAR (32),전열교환효율_냉방 VARCHAR (32),전열교환효율_난방 VARCHAR (32),습도교환효율_냉방 VARCHAR (32),습도교환효율_난방 VARCHAR (32),팬풍량 VARCHAR (32),팬정압 VARCHAR (32),팬동력 VARCHAR (32),모터제어 VARCHAR (32))"},
            {"User_DHWHP", "CREATE TABLE IF NOT EXISTS User_DHWHP (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),난방급탕 VARCHAR (32),급탕정격용량 VARCHAR (32),급탕정격COP VARCHAR (32),급탕정격소비전력 VARCHAR (32),난방정격용량 VARCHAR (32),난방정격COP VARCHAR (32),난방정격소비전력 VARCHAR (32),한랭지용량 VARCHAR (32),한랭지COP VARCHAR (32),한랭지소비전력 VARCHAR (32),대기전력 VARCHAR (32),대수 VARCHAR (32),설치 VARCHAR (32))"},
            //Construction 
            {"ConstructionBlind", "CREATE TABLE IF NOT EXISTS ConstructionBlind (ID INTEGER PRIMARY KEY AUTOINCREMENT, 번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),제품번호 VARCHAR (32),제품명 VARCHAR (32),종류 VARCHAR (32),설치 VARCHAR (32),투과수준 VARCHAR (32),색깔 VARCHAR (32),외부반사율 VARCHAR (32),내부반사율 VARCHAR (32),투과율 VARCHAR (32),흡수율 VARCHAR (32),제어방식1 VARCHAR (32),제어방식2 VARCHAR (32))"},
            {"ConstructionWall", "CREATE TABLE IF NOT EXISTS ConstructionWall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존외벽 VARCHAR (32),덧댐커튼월 VARCHAR (32),U적용방법 VARCHAR (32),직접간접 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교종류 VARCHAR (32),외장재색 VARCHAR (32),표면열전달저항기준 VARCHAR (32),선형점형 VARCHAR (32),A VARCHAR (32),B VARCHAR (32),C VARCHAR (32),PsiKai VARCHAR (32),단위면적당적용 VARCHAR (32),Rse VARCHAR (32),Rsi VARCHAR (32),두께합계 VARCHAR (32),열저항합계 VARCHAR (32),단열재두께 VARCHAR (32),재료1종류 VARCHAR (32),재료1두께 VARCHAR (32),재료2종류 VARCHAR (32),재료2두께 VARCHAR (32),재료3종류 VARCHAR (32),재료3두께 VARCHAR (32),재료4종류 VARCHAR (32),재료4두께 VARCHAR (32),재료5종류 VARCHAR (32),재료5두께 VARCHAR (32),재료6종류 VARCHAR (32),재료6두께 VARCHAR (32),재료7종류 VARCHAR (32),재료7두께 VARCHAR (32),재료8종류 VARCHAR (32),재료8두께 VARCHAR (32),재료9종류 VARCHAR (32),재료9두께 VARCHAR (32),재료10종류 VARCHAR (32),재료10두께 VARCHAR (32),흡수율 VARCHAR (32),열관류율 VARCHAR (32),열교가산치 VARCHAR (32),유효열관류율 VARCHAR (32),법규열관류율 VARCHAR (32))"},
            {"ConstructionCW", "CREATE TABLE IF NOT EXISTS ConstructionCW (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존커튼월 VARCHAR (32),Ucw적용방법 VARCHAR (32),직접간접 VARCHAR (32),프레임유형 VARCHAR (32),프레임종류 VARCHAR (32),고정유리종류 VARCHAR (32),개폐유리종류 VARCHAR (32),간봉종류 VARCHAR (32),설치유형 VARCHAR (32),설치종류 VARCHAR (32),LE_CL_V VARCHAR (32),패널적용유무 VARCHAR (32),패널종류 VARCHAR (32),패널유리종류 VARCHAR (32),LE_CL_V_Panel VARCHAR (32),출입문적용유무 VARCHAR (32),출입문프레임유형 VARCHAR (32),출입문프레임종류 VARCHAR (32),출입문유리종류 VARCHAR (32),출입문간봉종류 VARCHAR (32),LE_CL_V_Door VARCHAR (32),고정유리열관류율 VARCHAR (32),개폐유리열관류율 VARCHAR (32),태양열취득률 VARCHAR (32),빛투과율 VARCHAR (32),고정유리선형열관류율 VARCHAR (32),개폐유리선형열관류율 VARCHAR (32),고정프레임열관류율 VARCHAR (32),개폐프레임열관류율 VARCHAR (32),고정프레임두께 VARCHAR (32),개폐프레임두께 VARCHAR (32),패널열관류율 VARCHAR (32),패널유리열관류율 VARCHAR (32),패널열전도율 VARCHAR (32),패널흡수율 VARCHAR (32),패널선형열관류율 VARCHAR (32),패널두께 VARCHAR (32),출입문유리열관류율 VARCHAR (32),출입문태양열취득률 VARCHAR (32),출입문빛투과율 VARCHAR (32),출입문유리선형열관류율 VARCHAR (32),출입문프레임두께 VARCHAR (32),출입문프레임열관류율 VARCHAR (32),상부설치열관류율 VARCHAR (32),측면설치열관류율 VARCHAR (32),하부설치열관류율 VARCHAR (32),사이즈명칭 VARCHAR (32),커튼월면적 VARCHAR (32),너비 VARCHAR (32),높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32),패널면적 VARCHAR (32),패널둘레길이 VARCHAR (32),M_T프레임면적 VARCHAR (32),개폐창프레임면적 VARCHAR (32),출입문프레임면적 VARCHAR (32),출입문유리면적 VARCHAR (32),출입문유리둘레길이 VARCHAR (32),커튼월창열관류율 VARCHAR (32),유리부분열관류율 VARCHAR (32),패널부분열관류율 VARCHAR (32),출입문부분열관류율 VARCHAR (32),설치열교가산치 VARCHAR (32),커튼월창유효열관류율 VARCHAR (32),유리부분유효열관류율 VARCHAR (32),패널부분유효열관류율 VARCHAR (32),출입문부분유효열관류율 VARCHAR (32),유리부분유리면적비 VARCHAR (32),출입문부분유리면적비 VARCHAR (32),법규유리부분열관류율 VARCHAR (32),법규패널부분열관류율 VARCHAR (32),법규출입문부분열관류율 VARCHAR (32))"},
            {"ConstructionWindow", "CREATE TABLE IF NOT EXISTS ConstructionWindow (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),창호명칭 VARCHAR (32),Type VARCHAR (32),기존창호 VARCHAR (32),Uw적용방법 VARCHAR (32),직접간접 VARCHAR (32),프레임유형 VARCHAR (32),이중단창 VARCHAR (32),프레임재료 VARCHAR (32),프레임종류 VARCHAR (32),유리종류 VARCHAR (32),간봉종류 VARCHAR (32),설치유형 VARCHAR (32),설치종류 VARCHAR (32),LE_CL_V VARCHAR (32),유리열관류율 VARCHAR (32),태양열취득률 VARCHAR (32),빛투과율 VARCHAR (32),고정유리선형열관류율 VARCHAR (32),개폐유리선형열관류율 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),중간바프레임열관류율 VARCHAR (32),개폐부프레임두께 VARCHAR (32),고정부프레임두께 VARCHAR (32),중간바프레임두께 VARCHAR (32),상부설치열관류율 VARCHAR (32),측면설치열관류율 VARCHAR (32),하부설치열관류율 VARCHAR (32),창호열관류율 VARCHAR (32),법규열관류율 VARCHAR (32))"},
            {"ConstructionFloor", "CREATE TABLE IF NOT EXISTS ConstructionFloor (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존바닥 VARCHAR (32),기초설치 VARCHAR (32),U적용방법 VARCHAR (32),직접간접 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교종류 VARCHAR (32),표면열전달저항기준 VARCHAR (32),선형점형 VARCHAR (32),A VARCHAR (32),B VARCHAR (32),C VARCHAR (32),PsiKai VARCHAR (32),단위면적당적용 VARCHAR (32),Rse VARCHAR (32),Rsi VARCHAR (32),두께합계 VARCHAR (32),열저항합계 VARCHAR (32),단열재두께 VARCHAR (32),재료1종류 VARCHAR (32),재료1두께 VARCHAR (32),재료2종류 VARCHAR (32),재료2두께 VARCHAR (32),재료3종류 VARCHAR (32),재료3두께 VARCHAR (32),재료4종류 VARCHAR (32),재료4두께 VARCHAR (32),재료5종류 VARCHAR (32),재료5두께 VARCHAR (32),재료6종류 VARCHAR (32),재료6두께 VARCHAR (32),재료7종류 VARCHAR (32),재료7두께 VARCHAR (32),재료8종류 VARCHAR (32),재료8두께 VARCHAR (32),재료9종류 VARCHAR (32),재료9두께 VARCHAR (32),재료10종류 VARCHAR (32),재료10두께 VARCHAR (32),열관류율 VARCHAR (32),열교가산치 VARCHAR (32),유효열관류율 VARCHAR (32),법규열관류율 VARCHAR (32))"},
            {"ConstructionRoof", "CREATE TABLE IF NOT EXISTS ConstructionRoof (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존지붕 VARCHAR (32),U적용방법 VARCHAR (32),직접간접 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교종류 VARCHAR (32),외장재색 VARCHAR (32),표면열전달저항기준 VARCHAR (32),선형점형 VARCHAR (32),A VARCHAR (32),B VARCHAR (32),C VARCHAR (32),PsiKai VARCHAR (32),단위면적당적용 VARCHAR (32),Rse VARCHAR (32),Rsi VARCHAR (32),두께합계 VARCHAR (32),열저항합계 VARCHAR (32),단열재두께 VARCHAR (32),재료1종류 VARCHAR (32),재료1두께 VARCHAR (32),재료2종류 VARCHAR (32),재료2두께 VARCHAR (32),재료3종류 VARCHAR (32),재료3두께 VARCHAR (32),재료4종류 VARCHAR (32),재료4두께 VARCHAR (32),재료5종류 VARCHAR (32),재료5두께 VARCHAR (32),재료6종류 VARCHAR (32),재료6두께 VARCHAR (32),재료7종류 VARCHAR (32),재료7두께 VARCHAR (32),재료8종류 VARCHAR (32),재료8두께 VARCHAR (32),재료9종류 VARCHAR (32),재료9두께 VARCHAR (32),재료10종류 VARCHAR (32),재료10두께 VARCHAR (32),흡수율 VARCHAR (32),열관류율 VARCHAR (32),열교가산치 VARCHAR (32),유효열관류율 VARCHAR (32),법규열관류율 VARCHAR (32))"},
            {"ConstructionDoor", "CREATE TABLE IF NOT EXISTS ConstructionDoor (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),Type VARCHAR (32),기존출입문 VARCHAR (32),UD적용방법 VARCHAR (32),직접간접 VARCHAR (32),문짝제품 VARCHAR (32),출입문재질 VARCHAR (32),문틀내부 VARCHAR (32),문짝내부유형 VARCHAR (32),문짝색 VARCHAR (32),흡수율 VARCHAR (32),문짝단열재종류 VARCHAR (32),문짝두께 VARCHAR (32),문열관류율 VARCHAR (32),문틀상부측면열관류율 VARCHAR (32),문틀하부열관류율 VARCHAR (32),문면적 VARCHAR (32),문높이 VARCHAR (32),문길이 VARCHAR (32),유리적용유무 VARCHAR (32),유리가로 VARCHAR (32),유리세로 VARCHAR (32),유리종류 VARCHAR (32),유리면적 VARCHAR (32),유리열관류율 VARCHAR (32),유리반영문열관류율 VARCHAR (32),설치유형 VARCHAR (32),설치유형2 VARCHAR (32),상부선형열관류율 VARCHAR (32),측면부선형열관류율 VARCHAR (32),하부선형열관류율 VARCHAR (32),상부설치길이 VARCHAR (32),측면설치길이 VARCHAR (32),하부설치길이 VARCHAR (32),열교가산치 VARCHAR (32),문유효열관류율 VARCHAR (32),Door유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),법규열관류율 VARCHAR (32))"},
            {"SubWindow", "CREATE TABLE IF NOT EXISTS SubWindow (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),상위창호번호 VARCHAR (32),창호면적 VARCHAR (32),창호너비 VARCHAR (32),창호높이 VARCHAR (32),고정유리면적 VARCHAR (32),개폐유리면적 VARCHAR (32),개폐프레임면적 VARCHAR (32),고정프레임면적 VARCHAR (32),중간프레임면적 VARCHAR (32),고정유리둘레길이 VARCHAR (32),개폐유리둘레길이 VARCHAR (32),창호열관류율 VARCHAR (32),설치열교가산치 VARCHAR (32),창호유효열관류율 VARCHAR (32),유리면적비 VARCHAR (32),법규열관류율 VARCHAR (32))"},
            {"Import_WindowSize", "CREATE TABLE IF NOT EXISTS Import_WindowSize (ID INTEGER PRIMARY KEY AUTOINCREMENT,창호명칭 VARCHAR (32),창호면적 VARCHAR (32),창호너비 VARCHAR (32),창호높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),개폐프레임면적 VARCHAR (32),고정프레임면적 VARCHAR (32),중간프레임면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32))"},
            {"Import_CWSize", "CREATE TABLE IF NOT EXISTS Import_CWSize (ID INTEGER PRIMARY KEY AUTOINCREMENT,명칭 VARCHAR (32),커튼월면적 VARCHAR (32),너비 VARCHAR (32),높이 VARCHAR (32),고정창유리면적 VARCHAR (32),개폐창유리면적 VARCHAR (32),고정창유리둘레길이 VARCHAR (32),개폐창유리둘레길이 VARCHAR (32),패널면적 VARCHAR (32),패널둘레길이 VARCHAR (32),M_T프레임면적 VARCHAR (32),개폐창프레임면적 VARCHAR (32),출입문프레임면적 VARCHAR (32),출입문유리면적 VARCHAR (32),출입문유리둘레길이 VARCHAR (32))"},
            {"User_WindowFrame", "CREATE TABLE User_WindowFrame (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),프레임종류 VARCHAR (32),프레임재료 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),중간바프레임열관류율 VARCHAR (32),개폐부프레임두께 VARCHAR (32),고정부프레임두께 VARCHAR (32),중간바프레임두께 VARCHAR (32),시험성적서이미지 VARCHAR (32))"},
            {"User_CWFrame", "CREATE TABLE User_CWFrame (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),고정부프레임열관류율 VARCHAR (32),개폐부프레임열관류율 VARCHAR (32),패널엣지선형열관류율 VARCHAR (32),M_T프레임두께 VARCHAR (32),fr프레임두께 VARCHAR (32),시험성적서이미지 VARCHAR (32))"},
            {"User_Glass", "CREATE TABLE User_Glass (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),복층_삼중_단창 VARCHAR (32),아르곤_공기 VARCHAR (32),LE_CL_V VARCHAR (32),열관류율 VARCHAR (32),태양열취득율 VARCHAR (32),빛투과율 VARCHAR (32),외부반사율 VARCHAR (32),내부반사율 VARCHAR (32))"},
            {"User_DoubleGlass", "CREATE TABLE User_DoubleGlass (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),복층_삼중_단창 VARCHAR (32),아르곤_공기 VARCHAR (32),LE_CL_V VARCHAR (32),열관류율 VARCHAR (32),태양열취득율 VARCHAR (32),빛투과율 VARCHAR (32),외부반사율 VARCHAR (32),내부반사율 VARCHAR (32))"},
            {"User_WindowSpacer", "CREATE TABLE User_WindowSpacer (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),구분3 VARCHAR (32),고정유리_CL_선형열관류율 VARCHAR (32),개폐유리_CL_선형열관류율 VARCHAR (32),고정유리_LE_선형열관류율 VARCHAR (32),개폐유리_LE_선형열관류율 VARCHAR (32),LE_CL_V VARCHAR (32))"},
            {"User_CWSpacer", "CREATE TABLE User_CWSpacer (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분1 VARCHAR (32),구분3 VARCHAR (32),고정유리_CL_선형열관류율 VARCHAR (32),개폐유리_CL_선형열관류율 VARCHAR (32),고정유리_LE_선형열관류율 VARCHAR (32),개폐유리_LE_선형열관류율 VARCHAR (32),LE_CL_V VARCHAR (32))"},
            {"User_CWDoorFrame", "CREATE TABLE IF NOT EXISTS User_CWDoorFrame (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구분 VARCHAR (32),프레임열관류율 VARCHAR (32),프레임두께 VARCHAR (32))"},
            {"User_WindowInstall", "CREATE TABLE User_WindowInstall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),구분3 VARCHAR (32),구분4 VARCHAR (32),상부설치선형열관류율 VARCHAR (32),측면설치선형열관류율 VARCHAR (32),하부설치선형열관류율 VARCHAR (32))"},
            {"User_CWInstall", "CREATE TABLE User_CWInstall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),구분3 VARCHAR (32),상부설치선형열관류율 VARCHAR (32),측면설치선형열관류율 VARCHAR (32),하부설치선형열관류율 VARCHAR (32))"},
            {"User_Material", "CREATE TABLE IF NOT EXISTS User_Material (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),구분 VARCHAR (32),재료명 VARCHAR (32),종류2 VARCHAR (32),종류1 VARCHAR (32),열전도율 VARCHAR (32),밀도 VARCHAR (32),투습저항계수dry VARCHAR (32),투습저항계수wet VARCHAR (32),비열 VARCHAR (32),비고 VARCHAR (32))"},
            {"User_Blind", "CREATE TABLE IF NOT EXISTS User_Blind (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),종류 VARCHAR (32),설치 VARCHAR (32),투과수준 VARCHAR (32),색깔 VARCHAR (32),외부반사율 VARCHAR (32),내부반사율 VARCHAR (32),투과율 VARCHAR (32),흡수율 VARCHAR (32))"},
            {"User_DoorInstall", "CREATE TABLE User_DoorInstall (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),구분1 VARCHAR (32),구분2 VARCHAR (32),상부설치선형열관류율 VARCHAR (32),측면설치선형열관류율 VARCHAR (32),하부설치선형열관류율 VARCHAR (32))"},
            {"User_1DTB", "CREATE TABLE User_1DTB (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),제품명 VARCHAR (32),제조사 VARCHAR (32),구조체 VARCHAR (32),구조유형 VARCHAR (32),열교유형 VARCHAR (32),열교열관류율 VARCHAR (32),수직간격 VARCHAR (32),수평간격 VARCHAR (32))"},
            //3D       
            {"Blind_3D", "CREATE TABLE IF NOT EXISTS Blind_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,아이디 VARCHAR (32),번호 VARCHAR (32),프로젝트유형 VARCHAR (32),차양번호 VARCHAR (32),차양포함태양열취득률 VARCHAR (32),차양포함빛투과율 VARCHAR (32))"},
            {"Shade_3D", "CREATE TABLE IF NOT EXISTS Shade_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32), 프로젝트유형 VARCHAR (32),유형 VARCHAR (32),각도 VARCHAR (32),월 VARCHAR (32),음영계수 VARCHAR (32))"},
            {"ZoneGeneral_3D", "CREATE TABLE IF NOT EXISTS ZoneGeneral_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),존이름 VARCHAR (32),프로젝트유형 VARCHAR (32),층 VARCHAR (32),지면접합유형 VARCHAR (32),바닥면적 VARCHAR (32),층고 VARCHAR (32))"},
            {"ZoneEnvelope_3D", "CREATE TABLE IF NOT EXISTS ZoneEnvelope_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,아이디 VARCHAR (32),번호 VARCHAR (32),프로젝트유형 VARCHAR (32),층 VARCHAR (32),존 VARCHAR (32),외피유형 VARCHAR (32),커튼월부위 VARCHAR (32),면적 VARCHAR (32),인접존 VARCHAR (32),방위 VARCHAR (32),기울기 VARCHAR (32),우측면돌출각도 VARCHAR (32),좌측면돌출각도 VARCHAR (32),상부돌출각도 VARCHAR (32),주변요소음영각도 VARCHAR (32),구조체 VARCHAR (32),구조체번호 VARCHAR (32),우측면돌출길이 VARCHAR (32),좌측면돌출길이 VARCHAR (32),상부돌출길이 VARCHAR (32),주변요소음영길이 VARCHAR (32),벽체길이 VARCHAR (32),창호너비 VARCHAR (32),창호높이 VARCHAR (32),천창유무 VARCHAR (32),차양적용 VARCHAR (32),벽체높이 VARCHAR (32),상인방높이 VARCHAR (32))"},
            {"ThermalBridge_3D", "CREATE TABLE IF NOT EXISTS ThermalBridge_3D (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),열교항목 VARCHAR (32),열교길이 VARCHAR (32),선택열교 VARCHAR (32))"},
            {"User_TB", "CREATE TABLE IF NOT EXISTS User_TB (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB타입 VARCHAR (32),유형 VARCHAR (32),명칭 VARCHAR (32),구조체1 VARCHAR (32),구조체1_단열유형 VARCHAR (32),구조체2 VARCHAR (32),구조체2_단열유형 VARCHAR (32),값 VARCHAR (32))"},
            //Zone
            {"User_Lighting", "CREATE TABLE IF NOT EXISTS User_Lighting (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),등기구명칭 VARCHAR (32),램프유형 VARCHAR (32),제조사 VARCHAR (32),안정기_컨버터 VARCHAR (32),광속 VARCHAR (32),소비전력 VARCHAR (32),광효율 VARCHAR (32),조명계수 VARCHAR (32))"},
            {"User_Renew", "CREATE TABLE IF NOT EXISTS User_Renew (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),DB유형 VARCHAR (32),집광채광명칭 VARCHAR (32),집광채광종류 VARCHAR (32),제조사 VARCHAR (32),집광채광효율 VARCHAR (32),산광부가로길이 VARCHAR (32),산광부세로길이 VARCHAR (32),산광부면적 VARCHAR (32))"},
            {"ZoneGeneral_Form", "CREATE TABLE IF NOT EXISTS ZoneGeneral_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),프로젝트유형 VARCHAR (32),존이름 VARCHAR (32),실제어방식 VARCHAR (32),냉난방유무 VARCHAR (32),환기유무 VARCHAR (32),환기방식 VARCHAR (32),선택열회수기 VARCHAR (32),용도프로필 VARCHAR (32),순바닥면적 VARCHAR (32),천장고 VARCHAR (32),시작시간 VARCHAR (32),종료시간 VARCHAR (32),주이용일 VARCHAR (32),재실자수 VARCHAR (32),기기발열수준 VARCHAR (32),일일급탕요구량 VARCHAR (32),냉난방시간 VARCHAR (32),사용시간 VARCHAR (32),공조시간 VARCHAR (32),연이용일수 VARCHAR (32),재실밀도 VARCHAR (32),재실수준 VARCHAR (32),일일인체발열 VARCHAR (32),면적당인체발열 VARCHAR (32),일일기기발열 VARCHAR (32),면적당기기발열 VARCHAR (32),순체적 VARCHAR (32),환기횟수 VARCHAR (32),이용일환기량 VARCHAR (32),비이용일환기량  VARCHAR (32),천장축열선택 VARCHAR (32),외벽축열선택 VARCHAR (32),내벽축열선택 VARCHAR (32),바닥축열선택 VARCHAR (32),천장축열 VARCHAR (32),외벽축열 VARCHAR (32),내벽축열 VARCHAR (32),바닥축열 VARCHAR (32),천장면적 VARCHAR (32),외벽면적 VARCHAR (32),내벽면적 VARCHAR (32),바닥면적 VARCHAR (32),존축열성능 VARCHAR (32),존기밀타입 VARCHAR (32),기존존 VARCHAR (32),증축여부 VARCHAR (32))"},
            {"ZoneLighting_form", "CREATE TABLE IF NOT EXISTS ZoneLighting_form (ID INTEGER PRIMARY KEY AUTOINCREMENT, 번호 VARCHAR (32),프로젝트유형 VARCHAR (32),너비 VARCHAR (32),길이 VARCHAR (32),순바닥면적 VARCHAR (32),상인방높이 VARCHAR (32),작업면높이 VARCHAR (32),공간계수 VARCHAR (32),기준조도 VARCHAR (32),조명방식 VARCHAR (32),제어방식 VARCHAR (32),디밍유형 VARCHAR (32),조명밀도 VARCHAR (32),조명예상전력 VARCHAR (32),대기전력 VARCHAR (32),재실계수 VARCHAR (32),조도제어계수 VARCHAR (32),조명번호 VARCHAR (32),등기구명칭 VARCHAR (32), 램프유형 VARCHAR (32), 컨버터_안정기 VARCHAR (32), 광속 VARCHAR (32), 소비전력 VARCHAR (32), 광효율 VARCHAR (32), 조명계수 VARCHAR (32),표준광속 VARCHAR (32), 표준소비전력 VARCHAR (32), 사용자광속 VARCHAR (32), 사용자소비전력 VARCHAR (32),자연채광유형 VARCHAR (32),주향 VARCHAR (32),주창면적합 VARCHAR (32),주창유리종류 VARCHAR (32),주창아이디 VARCHAR (32),차양 VARCHAR (32),주광길이 VARCHAR (32),주광깊이 VARCHAR (32),주광면적 VARCHAR (32),비주광면적 VARCHAR (32),서브유형 VARCHAR (32),주창유리빛투과율 VARCHAR (32),주창유리면적비 VARCHAR (32),이중외피유리 VARCHAR (32),아트리움유리 VARCHAR (32),파사드유리빛투과율 VARCHAR (32),파사드너비 VARCHAR (32),파사드길이 VARCHAR (32),파사드높이 VARCHAR (32),천창유리각 VARCHAR (32),천창수평측면각 VARCHAR (32),천창장변부길이 VARCHAR (32),천창단변부길이 VARCHAR (32),천창수평상부높이 VARCHAR (32),집광채광체크 VARCHAR (32),집광채광번호 VARCHAR (32),집광채광명칭 VARCHAR (32),집광채광종류 VARCHAR (32),집광채광향 VARCHAR (32),집광채광각도 VARCHAR (32),집광채광효율 VARCHAR (32),집광채광면적 VARCHAR (32),표준길이1 VARCHAR (32),표준길이2 VARCHAR (32),사용자길이1 VARCHAR (32),사용자길이2 VARCHAR (32),사용자면적 VARCHAR (32),조명개수 VARCHAR (32))"},
            //System              
            {"HeatingSystem_Form", "CREATE TABLE IF NOT EXISTS HeatingSystem_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),존 VARCHAR (32),공조기 VARCHAR (32),설치위치 VARCHAR (32),공급환수온도 VARCHAR (32),복합설비유무 VARCHAR (32),주요설비 VARCHAR (32),보조설비1 VARCHAR (32),보조설비2 VARCHAR (32),보일러종류 VARCHAR (32),보일러대수 VARCHAR (32),태양열번호 VARCHAR (32),모듈개수 VARCHAR (32),모듈방위 VARCHAR (32),모듈기울기 VARCHAR (32),외기히트펌프번호 VARCHAR (32),외기히트펌프공급방식 VARCHAR (32),외기히트펌프제어방식 VARCHAR (32),외기히트펌프대수 VARCHAR (32),지열히트펌프번호 VARCHAR (32),지열히트펌프공급방식 VARCHAR (32),지열히트펌프제어방식 VARCHAR (32),지열히트펌프대수 VARCHAR (32),지하수히트펌프번호 VARCHAR (32),지하수히트펌프공급방식 VARCHAR (32),지하수히트펌프제어방식 VARCHAR (32),지하수히트펌프대수 VARCHAR (32),흡수식온수기번호 VARCHAR (32),흡수식온수기대수 VARCHAR (32),지역난방번호 VARCHAR (32),펌프유무 VARCHAR (32),펌프방식 VARCHAR (32),펌프1종류 VARCHAR (32),펌프2종류 VARCHAR (32),펌프1밸브 VARCHAR (32),펌프2밸브 VARCHAR (32),펌프1제어 VARCHAR (32),펌프2제어 VARCHAR (32),펌프1대수 VARCHAR (32),펌프2대수 VARCHAR (32),공급설비1종류 VARCHAR (32),공급설비2종류 VARCHAR (32),축열유무 VARCHAR (32),축열펌프유무 VARCHAR (32),축열펌프 VARCHAR (32),축열용량 VARCHAR (32),배관관경 VARCHAR (32),배관보온두께 VARCHAR (32),보온열전도율 VARCHAR (32),배관보온재 VARCHAR (32),노출배관길이 VARCHAR (32),연료전지번호 VARCHAR (32),연료전지대수 VARCHAR (32))"},
            {"Heating_ce_Form", "CREATE TABLE IF NOT EXISTS Heating_ce_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),프로젝트유형 VARCHAR (32),난방시스템 VARCHAR (32),공급설비종류 VARCHAR (32),공급설비 VARCHAR (32),설치위치 VARCHAR (32),가동시간 VARCHAR (32),부하율 VARCHAR (32))"},
            {"DHWSystem_Form", "CREATE TABLE IF NOT EXISTS DHWSystem_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),존 VARCHAR (32),설치위치 VARCHAR (32),공급환수온도 VARCHAR (32),복합설비유무 VARCHAR (32),주요설비 VARCHAR (32),보조설비1 VARCHAR (32),보조설비2 VARCHAR (32),보일러종류 VARCHAR (32),보일러대수 VARCHAR (32),태양열번호 VARCHAR (32),모듈개수 VARCHAR (32),모듈방위 VARCHAR (32),모듈기울기 VARCHAR (32),지역난방번호 VARCHAR (32),펌프유무 VARCHAR (32),펌프방식 VARCHAR (32),펌프1종류 VARCHAR (32),펌프2종류 VARCHAR (32),펌프1밸브 VARCHAR (32),펌프2밸브 VARCHAR (32),펌프1제어 VARCHAR (32),펌프2제어 VARCHAR (32),펌프1대수 VARCHAR (32),펌프2대수 VARCHAR (32),축열유무 VARCHAR (32),축열펌프유무 VARCHAR (32),축열펌프 VARCHAR (32),축열용량 VARCHAR (32),축열유형 VARCHAR (32),배관관경 VARCHAR (32),배관보온두께 VARCHAR (32),보온열전도율 VARCHAR (32),배관보온재 VARCHAR (32),노출배관길이 VARCHAR (32),히트펌프번호 VARCHAR (32),히트펌프제어방식 VARCHAR (32),히트펌프대수 VARCHAR (32),연료전지번호 VARCHAR (32),연료전지대수 VARCHAR (32))"},
            {"AHUSystem_Form", "CREATE TABLE IF NOT EXISTS AHUSystem_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),유형 VARCHAR (32),시스템번호 VARCHAR (32),설치위치 VARCHAR (32),풍량제어 VARCHAR (32),누기시험방법 VARCHAR (32),누기등급1 VARCHAR (32),누기등급2 VARCHAR (32),공조기단열두께 VARCHAR (32),TAB실시유무 VARCHAR (32),덕트누기수준 VARCHAR (32),OA덕트길이 VARCHAR (32),EA덕트길이 VARCHAR (32),SA덕트길이 VARCHAR (32),RA덕트길이 VARCHAR (32),덕트단열두께 VARCHAR (32),덕트관경 VARCHAR (32),덕트단열재 VARCHAR (32),덕트단열재열전도율 VARCHAR (32),예열예냉유형 VARCHAR (32),프리히터제어유형 VARCHAR (32),프리히터용량 VARCHAR (32),토양유형 VARCHAR (32),지중깊이 VARCHAR (32),쿨튜브관경 VARCHAR (32),쿨튜브두께 VARCHAR (32),쿨튜브길이 VARCHAR (32),쿨튜브재질 VARCHAR (32))"},
            //냉방설비 프로젝트유형 컬럼 추가해야 함             
            {"Cooling_ce_Form", "CREATE TABLE IF NOT EXISTS Cooling_ce_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32), 프로젝트유형 VARCHAR (32), 냉방시스템 VARCHAR (32),공급설비종류 VARCHAR (32),공급설비 VARCHAR (32), 가동시간 VARCHAR (32), 용량 VARCHAR (32), 소비전력 VARCHAR (32), 부하율 VARCHAR (32))"},
            {"User_AirCooler", "CREATE TABLE IF NOT EXISTS User_AirCooler (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),냉방출력 VARCHAR (32),냉방소비전력 VARCHAR (32),EER VARCHAR (32),압축기 VARCHAR (32),연료 VARCHAR (32),대기전력 VARCHAR (32),대수 VARCHAR (32),설치 VARCHAR (32), 부하측공급형식 VARCHAR (32), 증발기 VARCHAR (32), 냉수입구온도 VARCHAR (32), 냉수출구온도 VARCHAR (32), 송풍기전력 VARCHAR (32))"},
            {"User_WaterCooler", "CREATE TABLE IF NOT EXISTS User_WaterCooler (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),냉방출력 VARCHAR (32),냉방소비전력 VARCHAR (32),EER VARCHAR (32),압축기 VARCHAR (32),연료 VARCHAR (32),대기전력 VARCHAR (32),대수 VARCHAR (32),설치 VARCHAR (32), 증발기 VARCHAR (32), 냉수입구온도 VARCHAR (32), 냉수출구온도 VARCHAR (32))"},
            {"User_CoolingTop", "CREATE TABLE IF NOT EXISTS User_CoolingTop (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),DB유형 VARCHAR (32),명칭 VARCHAR (32),형식 VARCHAR (32),냉각능력 VARCHAR (32),냉각수량 VARCHAR (32),입구온도 VARCHAR (32),출구온도 VARCHAR (32),소비전력 VARCHAR (32),제어유형 VARCHAR (32),팬유형 VARCHAR (32), 냉방전력소비계수 VARCHAR (32), 대수 VARCHAR (32), 대기전력 VARCHAR (32), 설치 VARCHAR (32))"},
            {"CoolingSystem_Form", "CREATE TABLE IF NOT EXISTS CoolingSystem_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32), 프로젝트유형 VARCHAR (32), 명칭 VARCHAR(32), 공급존 VARCHAR(32), 공급AHU VARCHAR(32), 냉방설비 VARCHAR(32),  열원설비 VARCHAR(32), 냉방유닛 VARCHAR(32), 제어유형 VARCHAR(32), 외기냉방시스템 VARCHAR(32), 설치대수 VARCHAR(32), 저장탱크 VARCHAR(32), 저장유형 VARCHAR(32), 압축기 VARCHAR(32), 펌프유무 VARCHAR (32),냉수펌프방식 VARCHAR (32),냉수펌프1 VARCHAR (32),냉수펌프2 VARCHAR (32),냉각수펌프방식 VARCHAR(32), 냉각수펌프1 VARCHAR (32),냉각수펌프2 VARCHAR (32),공급설비1종류 VARCHAR (32),공급설비2종류 VARCHAR (32),공급설비3종류 VARCHAR (32),공급설비4종류 VARCHAR (32),냉방출력 VARCHAR(32), 냉방성능 VARCHAR(32), 냉각탑 VARCHAR(32), 냉각탑개수 VARCHAR(32))"},
            {"Cooling_ce_Form_Element", "CREATE TABLE IF NOT EXISTS Cooling_ce_Form_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32), 프로젝트유형 VARCHAR (32), 냉방시스템 VARCHAR (32),공급설비종류 VARCHAR (32),공급설비 VARCHAR (32), 가동시간 VARCHAR (32), 용량 VARCHAR (32), 소비전력 VARCHAR (32), 부하율 VARCHAR (32))"},
            //신재생
            {"PV_Form", "CREATE TABLE IF NOT EXISTS PV_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),모듈번호 VARCHAR (32), 용량 VARCHAR (32), 인버터번호 VARCHAR (32),인버터효율 VARCHAR (32),배터리번호 VARCHAR (32),배터리용량 VARCHAR (32),계통유형 VARCHAR (32),개수 VARCHAR (32),면적 VARCHAR (32),방위 VARCHAR (32),기울기 VARCHAR (32),통풍유무  VARCHAR (32), 지형물거리 VARCHAR (32),지형물높이 VARCHAR (32), 어레이높이  VARCHAR (32), fperf  VARCHAR (32), 설치 VARCHAR (32), 기존PV VARCHAR (32))"},
            {"PV_Result", "CREATE TABLE IF NOT EXISTS PV_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),일사량 VARCHAR (32),PV생산량 VARCHAR (32),매칭계수 VARCHAR (32),배터리손실 VARCHAR (32),그리드생산량 VARCHAR (32),건물사용량 VARCHAR (32))"},
            {"FuelCell_Form", "CREATE TABLE IF NOT EXISTS FuelCell_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),연료전지 VARCHAR (32),생산유형 VARCHAR (32),시작시간 VARCHAR (32),종료시간 VARCHAR (32),사용시간 VARCHAR (32),주이용일 VARCHAR (32),설치대수 VARCHAR (32), 급탕설비 VARCHAR (32), 난방설비 VARCHAR (32))"},
            {"WindPower_Form", "CREATE TABLE IF NOT EXISTS WindPower_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),명칭 VARCHAR (32),풍력 VARCHAR (32),주변환경 VARCHAR (32),설치높이 VARCHAR (32),인버터제품 VARCHAR (32),인버터 VARCHAR (32), 설치대수 VARCHAR (32))"},

            {"SolarTherm_Form", "CREATE TABLE IF NOT EXISTS SolarTherm_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),프로젝트유형 VARCHAR (32),태양열번호 VARCHAR (32),설비번호 VARCHAR (32), 적용설비 VARCHAR (32), 적용유형 VARCHAR (32), 모듈개수 VARCHAR (32),방위 VARCHAR (32),기울기 VARCHAR (32),설치 VARCHAR (32),지형물거리 VARCHAR (32),지형물높이 VARCHAR (32),모듈높이 VARCHAR (32), 기존태양열 VARCHAR (32))"},
            
                       
            //Calc
            {"Zone_LightResult", "CREATE TABLE IF NOT EXISTS Zone_LightResult (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),ITr VARCHAR (32),IRD VARCHAR (32),ISh_Ish VARCHAR (32),Ish_In_At VARCHAR (32),Wi VARCHAR (32),Ish_GDF VARCHAR (32),Ish VARCHAR (32),f_τeff_SNA VARCHAR (32),f_D VARCHAR (32),f_nearD VARCHAR (32),f_DCA VARCHAR (32),f_dclass VARCHAR (32),f_nearEm_SNA VARCHAR (32),f_fd_sna VARCHAR (32),f_fd_sa VARCHAR (32),f_nearEm_DC VARCHAR (32),f_fd_c VARCHAR (32),f_FDS VARCHAR (32),f_FD VARCHAR (32),as_bs VARCHAR (32),hs_bs VARCHAR (32),hg_hw VARCHAR (32),normal_ηR VARCHAR (32),saw_ηR VARCHAR (32),r_DSNA VARCHAR (32),r_DSA VARCHAR (32),r_dclass VARCHAR (32),r_nearEm_FDS VARCHAR (32),r_fd_sna VARCHAR (32),r_fd_sa VARCHAR (32),r_nearEm_DC VARCHAR (32),r_fd_c VARCHAR (32),r_FDS VARCHAR (32),r_FD VARCHAR (32),Sunlight_SCW VARCHAR (32),Sunlight_PjSC VARCHAR (32),Final_kWh VARCHAR (32))"},
            {"Zone_Envelope_Result", "CREATE TABLE IF NOT EXISTS Zone_Envelope_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),외피번호 VARCHAR (32),존번호 VARCHAR (32),구조체번호 VARCHAR (32),외피유형 VARCHAR (32),커튼월유형 VARCHAR (32),직접간접 VARCHAR (32),난방_냉방 VARCHAR (32),비이용일_이용일 VARCHAR (32),월 VARCHAR (32),HT VARCHAR (32),HT_TB VARCHAR (32),QTsink VARCHAR (32),QTsource VARCHAR (32),QT_TB_sink VARCHAR (32),QT_TB_source VARCHAR (32),QTsink_tot VARCHAR (32),QTsource_tot VARCHAR (32),QSsink VARCHAR (32),QSsource VARCHAR (32))"},
            {"Zone_Alt_Result", "CREATE TABLE IF NOT EXISTS Zone_Alt_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,검토유형 VARCHAR (32),번호 VARCHAR (32), 이름 VARCHAR (32), 난방_냉방 VARCHAR (32), 비이용일_이용일 VARCHAR (32), 월 VARCHAR (32),Qb_day VARCHAR (32),Qb_mth VARCHAR (32),Qb_a VARCHAR (32), Q_max VARCHAR (32), t_max VARCHAR (32), 비냉난방존온도 VARCHAR (32))"},
            {"Zone_HCneed_Result", "CREATE TABLE IF NOT EXISTS Zone_HCneed_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32), 이름 VARCHAR (32), 난방_냉방 VARCHAR (32), 비이용일_이용일 VARCHAR (32), 월 VARCHAR (32), HT_tot VARCHAR (32), HT_InWall VARCHAR (32),HT_Slab VARCHAR (32),HT_Wall VARCHAR (32), HT_Roof VARCHAR (32), HT_Floor VARCHAR (32), HT_GWall VARCHAR (32), HT_Door VARCHAR (32), HT_Win VARCHAR (32), HT_CW VARCHAR (32), HT_Di_Wall VARCHAR (32), HT_Indi_Wall VARCHAR (32), HT_Di_Roof VARCHAR (32), HT_Indi_Roof VARCHAR (32), HT_Di_Win VARCHAR (32), HT_Indi_Win VARCHAR (32), HT_Di_Door VARCHAR (32), HT_Indi_Door VARCHAR (32), HT_TB_tot VARCHAR (32), HT_TB_Wall VARCHAR (32), HT_TB_Roof VARCHAR (32), HT_TB_Floor VARCHAR (32), HT_TB_Gwall VARCHAR (32), HT_TB_Win VARCHAR (32), HT_TB_Door VARCHAR (32), HT_TB_CW VARCHAR (32), nmech VARCHAR (32), nz VARCHAR (32), ninf VARCHAR (32), nwin VARCHAR (32),  HV_tot VARCHAR (32), HV_inf VARCHAR (32), HV_win VARCHAR (32), HV_z VARCHAR (32), HV_mech VARCHAR (32), H_tot VARCHAR (32), tao VARCHAR (32), dwe_mth VARCHAR (32), dwd_mth VARCHAR (32), theta_i VARCHAR (32), theta_e VARCHAR (32), QTsink_tot VARCHAR (32), QT_u_sink VARCHAR (32), QTsink_Wall VARCHAR (32), QTsink_Roof VARCHAR (32), QTsink_Floor VARCHAR (32), QTsink_GWall VARCHAR (32), QTsink_Door VARCHAR (32), QTsink_Win VARCHAR (32), QTsink_CW VARCHAR (32), QTsource_tot VARCHAR (32), QT_u_source VARCHAR (32), QTsource_Wall VARCHAR (32), QTsource_Roof VARCHAR (32), QTsource_Floor VARCHAR (32), QTsource_GWall VARCHAR (32), QTsource_Door VARCHAR (32), QTsource_Win VARCHAR (32), QTsource_CW VARCHAR (32), QSopsink_tot VARCHAR (32), QSopsource_tot VARCHAR (32), QStr_tot VARCHAR (32), QSopsink_Wall VARCHAR (32), QSopsink_Roof VARCHAR (32), QSopsink_Door VARCHAR (32), QSopsink_CW_p VARCHAR (32), QSopsource_Wall VARCHAR (32), QSopsource_Roof VARCHAR (32), QSopsource_Door VARCHAR (32), QSopsource_CW_p VARCHAR (32), QStr_Win VARCHAR (32), QStr_CW VARCHAR (32), QVsink_tot VARCHAR (32), QV_inf_sink VARCHAR (32), QV_win_sink VARCHAR (32), QV_z_sink VARCHAR (32), QV_mech_sink VARCHAR (32), QVsource_tot VARCHAR (32), QV_inf_source VARCHAR (32), QV_win_source VARCHAR (32), QV_z_source VARCHAR (32), QV_mech_source VARCHAR (32), Q_DHU_win VARCHAR (32), Q_DHU_mech VARCHAR (32), Q_DHU_tot VARCHAR (32), QI_tot VARCHAR (32), QI_L VARCHAR (32), QI_P VARCHAR (32), QI_fac VARCHAR (32), QI_Humidity VARCHAR (32), Qsink VARCHAR (32), Qsource VARCHAR (32), gamma VARCHAR (32), a VARCHAR (32), eta VARCHAR (32), dQc_b VARCHAR (32), dQc_sink VARCHAR (32),Qb_day VARCHAR (32),Qb_mth VARCHAR (32),Qb_a VARCHAR (32), Q_max VARCHAR (32), t_max VARCHAR (32), 비냉난방존온도 VARCHAR (32))"},
            {"HeatingSystem_Result", "CREATE TABLE IF NOT EXISTS HeatingSystem_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32), 번호 VARCHAR (32), 월 VARCHAR (32), Qhb_mth_sum VARCHAR (32),  Qh_max_sum VARCHAR (32), Qh_a_sum VARCHAR (32), th_op_day_avg VARCHAR (32),  theta_i_h_set_avg VARCHAR (32), th_avg VARCHAR (32), dop_mth_avg VARCHAR (32),thrL VARCHAR (32),thrL_day VARCHAR (32),dhrB VARCHAR (32),fLNA VARCHAR (32),fLwe VARCHAR (32),beta_h_ce VARCHAR (32),beta_h_d VARCHAR (32),beta_h_s VARCHAR (32),beta_h_gen VARCHAR (32),theta_av_ce VARCHAR (32),theta_av_d VARCHAR (32),theta_av_s VARCHAR (32),theta_av_gen VARCHAR (32),dtheta_ce VARCHAR (32),dtheta_d VARCHAR (32),dtheta_s VARCHAR (32),dtheta_gen VARCHAR (32),dtheta_ce1 VARCHAR (32),dtheta_ce2 VARCHAR (32),Psi_pipe VARCHAR (32),L VARCHAR (32),Qs_po_day VARCHAR (32),Vs VARCHAR (32),Qh_gen_day VARCHAR (32),Pgen_Pn VARCHAR (32),Pgen_Pint VARCHAR (32),Pgen_P0 VARCHAR (32),eta_gen_Pn VARCHAR (32),eta_gen_Pint VARCHAR (32),fpint_Air VARCHAR (32),Qh_outg_sngminus7 VARCHAR (32),Qh_outg_sng2 VARCHAR (32),Qh_outg_sng7 VARCHAR (32),COPminus7 VARCHAR (32),COP2 VARCHAR (32),COP7 VARCHAR (32),Qh_ce VARCHAR (32),Qh_d VARCHAR (32),Qh_s VARCHAR (32),Qh_gen VARCHAR (32),Qh_outg VARCHAR (32),Qh_f VARCHAR (32),Wh_ce VARCHAR (32),Wh_d VARCHAR (32),Wh_s VARCHAR (32),Wh_g VARCHAR (32),Qh_sol VARCHAR (32),연료 VARCHAR (32))"},
            {"CoolingSystem_Result", "CREATE TABLE IF NOT EXISTS CoolingSystem_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32), 명칭 VARCHAR (32), 냉방설비 VARCHAR (32), 냉방출력 VARCHAR (32), 냉방성능 VARCHAR (32), 압축기종류 VARCHAR (32), 냉수출구온도 VARCHAR (32), 대기전력 VARCHAR (32), 설치대수 VARCHAR (32), Fuel VARCHAR (32),개수_z VARCHAR (32), QCb_a_z VARCHAR (32), QC_Max_z VARCHAR (32), 공급설비1_z VARCHAR (32), 공급설비2_z VARCHAR (32), A_z VARCHAR (32), 개수_ahu VARCHAR (32), QCb_a_ahu VARCHAR (32), QC_Max_ahu VARCHAR (32), 공급설비1_ahu VARCHAR (32), 공급설비2_ahu VARCHAR (32), A_ahu VARCHAR (32), 열원설비 VARCHAR (32), CTpower VARCHAR (32), CSWin VARCHAR (32), CSWout VARCHAR (32), QCb_a VARCHAR (32), QCa_ce VARCHAR (32), QCa_d VARCHAR (32), QCa_s VARCHAR (32), QCa_out VARCHAR (32), QCa_f VARCHAR (32), QCa_p VARCHAR (32), QCa_CO2 VARCHAR (32), Sto_Tank VARCHAR (32), Sto_Type VARCHAR (32), P1power VARCHAR (32), P2power VARCHAR (32), Pump1Valve VARCHAR (32), SP1power VARCHAR (32), SP2power VARCHAR (32), SPValve VARCHAR (32), 월 VARCHAR (32), QC_out_z VARCHAR (32), QC_ce_z VARCHAR (32), QC_d_z VARCHAR (32), QC_s_z VARCHAR (32), QC_nd_z VARCHAR (32), QC_out_ahu VARCHAR (32), QC_ce_ahu VARCHAR (32), QC_d_ahu VARCHAR (32), QC_s_ahu VARCHAR (32), QC_nd_ahu VARCHAR (32), QC_f VARCHAR (32), SEER_c VARCHAR (32), EER_c VARCHAR (32), QC_out VARCHAR (32), QC_ce VARCHAR (32), QC_d VARCHAR (32), QC_s VARCHAR (32), QC_nd VARCHAR (32), W VARCHAR (32), W_g VARCHAR (32), W_ce VARCHAR (32), W_d VARCHAR (32), W_s VARCHAR (32))"},
            {"DHWSystem_Result", "CREATE TABLE IF NOT EXISTS DHWSystem_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),Qwb_mth_sum VARCHAR (32),theta_ih_avg VARCHAR (32),Qw_a_sum VARCHAR (32),th_op_day_avg VARCHAR (32),theta_i_h_set_avg VARCHAR (32),dop_mth_avg VARCHAR (32),Qw_d VARCHAR (32),Qw_s VARCHAR (32),Qw_gen VARCHAR (32),Qw_outg VARCHAR (32),Qw_f VARCHAR (32),Ww_d VARCHAR (32),Ww_s VARCHAR (32),Ww_g VARCHAR (32),Qw_gen_day VARCHAR (32),Qw_gen_p0_day VARCHAR (32),eta_pn_w VARCHAR (32),Qw_sol VARCHAR (32),연료 VARCHAR (32))"},
            {"AHUSystem_Result", "CREATE TABLE IF NOT EXISTS AHUSystem_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32),난방_냉방 VARCHAR (32),월 VARCHAR (32),공조요구량 VARCHAR (32),가습요구량 VARCHAR (32),급기팬보조에너지 VARCHAR (32),배기팬보조에너지 VARCHAR (32),가습보조에너지 VARCHAR (32),프리히팅보조에너지 VARCHAR (32),theta_vmech VARCHAR (32),Vvmech VARCHAR (32),Vvmech_leak VARCHAR (32),theta_SA_prh VARCHAR (32),theta_OA_du VARCHAR (32),theta_RA_du VARCHAR (32),theta_SA_hr VARCHAR (32),theta_SA_rca VARCHAR (32),theta_SA_du VARCHAR (32),X_iset VARCHAR (32),X_SA_prh VARCHAR (32),X_SA_hr VARCHAR (32),X_SA_rca VARCHAR (32),Vmin_tot VARCHAR (32),Qb_mth_tot VARCHAR (32),Qmax_tot VARCHAR (32),theta_iset_avg VARCHAR (32),dvmech_avg VARCHAR (32),tvmech_avg VARCHAR (32),Q_gnd VARCHAR (32),Q_prh VARCHAR (32),Q_loss_OA_du VARCHAR (32),Q_loss_EA_du VARCHAR (32),Q_loss_SA_du VARCHAR (32),dtheta_prh VARCHAR (32),dtheta_du_OA VARCHAR (32),dtheta_du_RA VARCHAR (32),dtheta_hr VARCHAR (32),dtheta_rca VARCHAR (32),dtheta_du_EA VARCHAR (32),dtheta_du_SA VARCHAR (32),flea_du VARCHAR (32),flea_ahu VARCHAR (32),fins_ahu VARCHAR (32),theta_defrost VARCHAR (32),theta_sur_nc VARCHAR (32),Hduct_OA VARCHAR (32),Hduct_RA VARCHAR (32),Hduct_EA VARCHAR (32),Hduct_SA VARCHAR (32))"},
            {"WindPower_Result", "CREATE TABLE IF NOT EXISTS WindPower_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),h VARCHAR (32), Pwind VARCHAR (32), Pwps VARCHAR (32),Qfwps VARCHAR (32))"},
            {"FinalEnergy_Result", "CREATE TABLE IF NOT EXISTS FinalEnergy_Result (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),연료 VARCHAR (32),난방 REAL,냉방 REAL,급탕 REAL,조명 REAL,공조 REAL,기저에너지 REAL,신재생에너지 REAL,총에너지소요량 REAL)"},
             //ALT
            {"FinalEnergy_Result_Rule", "CREATE TABLE IF NOT EXISTS FinalEnergy_Result_Rule (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),검토유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),연료 VARCHAR (32),난방 REAL,냉방 REAL,급탕 REAL,조명 REAL,공조 REAL,기저에너지 REAL,신재생에너지 REAL,총에너지소요량 REAL)"},
            {"FinalEnergy_Result_Element", "CREATE TABLE IF NOT EXISTS FinalEnergy_Result_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),검토유형 VARCHAR (32),번호 VARCHAR (32),월 VARCHAR (32),연료 VARCHAR (32),난방 REAL,냉방 REAL,급탕 REAL,조명 REAL,공조 REAL,기저에너지 REAL,신재생에너지 REAL,총에너지소요량 REAL)"},
            {"Heating_ce_Form_Element", "CREATE TABLE IF NOT EXISTS Heating_ce_Form_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,존번호 VARCHAR (32),난방시스템 VARCHAR (32),공급설비종류 VARCHAR (32),공급설비 VARCHAR (32),설치위치 VARCHAR (32),가동시간 VARCHAR (32),부하율 VARCHAR (32))"},
            {"Heating_Result_Element" ,"CREATE TABLE IF NOT EXISTS Heating_Result_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,검토유형 VARCHAR (32),난방시스템 VARCHAR (32),기존존번호 VARCHAR (32),계획존번호 VARCHAR (32),공급설비 VARCHAR (32),부하율 VARCHAR (32),연료 VARCHAR (32),난방소요량 VARCHAR (32))"},
            {"Cooling_Result_Element" ,"CREATE TABLE IF NOT EXISTS Cooling_Result_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,검토유형 VARCHAR (32),냉방시스템 VARCHAR (32),기존존번호 VARCHAR (32),계획존번호 VARCHAR (32),공급설비 VARCHAR (32),부하율 VARCHAR (32),연료 VARCHAR (32),냉방소요량 VARCHAR (32))"},
            {"DHWSystem_Result_Element" ,"CREATE TABLE IF NOT EXISTS DHWSystem_Result_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,검토유형 VARCHAR (32),급탕시스템 VARCHAR (32),기존존번호 VARCHAR (32),계획존번호 VARCHAR (32),연료 VARCHAR (32),급탕소요량 VARCHAR (32))"},
            {"Light_Result_Element" ,"CREATE TABLE IF NOT EXISTS Light_Result_Element (ID INTEGER PRIMARY KEY AUTOINCREMENT,검토유형 VARCHAR (32),존번호 VARCHAR (32),조명번호 VARCHAR (32),조명소요량 VARCHAR (32))"},
            //Optimal
            {"FinalEnergy_Result_Optimal", "CREATE TABLE IF NOT EXISTS FinalEnergy_Result_Optimal (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),검토유형 VARCHAR (32),리모델링안 VARCHAR (32),리모델링값 REAL,월 VARCHAR (32),연료 VARCHAR (32),난방 REAL,냉방 REAL,급탕 REAL,조명 REAL,공조 REAL,기저에너지 REAL,신재생에너지 REAL,총에너지소요량 REAL)"},
            {"Optimal_Form", "CREATE TABLE IF NOT EXISTS Optimal_Form (ID INTEGER PRIMARY KEY AUTOINCREMENT,번호 VARCHAR (32),명칭 VARCHAR (32),총공사비 VARCHAR (32),순공사비 VARCHAR (32),종합점수 VARCHAR (32),요소기술1 VARCHAR (32),리모델링안1 VARCHAR (32),요소기술2 VARCHAR (32),리모델링안2 VARCHAR (32),요소기술3 VARCHAR (32),리모델링안3 VARCHAR (32),요소기술4 VARCHAR (32),리모델링안4 VARCHAR (32),요소기술5 VARCHAR (32),리모델링안5 VARCHAR (32),요소기술6 VARCHAR (32),리모델링안6 VARCHAR (32),요소기술7 VARCHAR (32),리모델링안7 VARCHAR (32),요소기술8 VARCHAR (32),리모델링안8 VARCHAR (32),요소기술9 VARCHAR (32),리모델링안9 VARCHAR (32),요소기술10 VARCHAR (32),리모델링안10 VARCHAR (32))"},
            {"Optimal_PreResult", "CREATE TABLE IF NOT EXISTS Optimal_PreResult (ID INTEGER PRIMARY KEY AUTOINCREMENT,프로젝트번호 VARCHAR (32),프로젝트유형 VARCHAR (32),검토유형 VARCHAR (32),리모델링안 VARCHAR (32),리모델링값유형 VARCHAR (32),리모델링값 VARCHAR (32),순공사비 VARCHAR (32),재료비 VARCHAR (32),노무비 VARCHAR (32),경비 VARCHAR (32),에너지절감량 VARCHAR (32),에너지절감률 VARCHAR (32),에너지점수 VARCHAR (32),쾌적성점수 VARCHAR (32),적법성점수 VARCHAR (32),경제성점수 VARCHAR (32),종합점수 REAL)"}
        };

        Dictionary<type, string> dbnames = new Dictionary<type, string>() {
            {type.BaseDB_HCneed, "basedb_hcneed.sqlite"},
            {type.BaseDB_Lighting, "basedb_lighting.sqlite"},
            {type.BaseDB_Heating, "basedb_heating.sqlite"},
            {type.BaseDB_Cooling, "basedb_cooling.sqlite"},
            {type.BaseDB_AHU, "basedb_ahu.sqlite"},
            {type.BaseDB_RESystem, "basedb_resystem.sqlite"},
            {type.BaseDB_Optimal, "basedb_optimal.sqlite"}
        };

        public bool openDB(string projPath)
        {
            foreach (var dbname in dbnames)
            {
                SecureSQLite.OpenDB(dbname.Value, (int)dbname.Key);
            }

            if (GetFileSize(projPath) <= 0)
            {
                File.Copy("templ.sqlite", projPath, true);
            }

            if (SecureSQLite.OpenDB(projPath, (int)type.ProjDB) != 1)
            {
                closeBaseDB();
                return false;
            }

            if (SecureSQLite.OpenMemoryDB((int)type.CalcDB) != 1)
            {
                closeBaseDB();
                SecureSQLite.CloseDB((int)type.ProjDB);
                return false;
            }

            projDBPath = projPath;

            return true;
        }
        public bool openPListDB(string? gPath)
        {
            SecureSQLite.SetInfo(gPath, false);  

            return !!(SecureSQLite.OpenDB("projects.sqlite", (int)type.ProjListDB) == 1);
        }
        public void closePListDB()
        {
            SecureSQLite.SaveDB("projects.sqlite", (int)type.ProjListDB);
            SecureSQLite.CloseDB((int)type.ProjListDB);
        }
        public void savePListDB()
        {
            SecureSQLite.SaveDB("projects.sqlite", (int)type.ProjListDB);
        }
        public void closeBaseDB()
        {
            foreach (var dbname in dbnames)
            {
                SecureSQLite.CloseDB((int)dbname.Key);
            }
        }
        public void closeDB()
        {
            closeBaseDB();

            SecureSQLite.CloseDB((int)type.ProjDB);
            SecureSQLite.CloseDB((int)type.CalcDB);
        }
        public void saveProject()
        {
            SecureSQLite.SaveDB(projDBPath, (int)type.ProjDB);
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
                SecureSQLite.ExecuteSQL((int)dbType, exec);
            }
        }
        public void executeSQL(string projName, string query)
        {
            if (projDBPath.IndexOf(projName + ".sqlite") != -1)
            {
                executeSQL(type.ProjDB, query);
                return;
            }
            if (SecureSQLite.OpenDB("projects\\" + projName + ".sqlite", customDB) == 1)
            {
                SecureSQLite.ExecuteSQL(customDB, query);
            }
        }
        public string QuerySQL(int dbType, string query)
        {
            IntPtr ptr = SecureSQLite.QuerySQL(dbType, query);
            string? ret = Marshal.PtrToStringUni(ptr);
            Marshal.FreeHGlobal(ptr);

            if (ret != null)
            {
                return ret.Trim();
            }
            else
            {
                return "[]";
            }
        }
        public string[][] querySQL(type dbType, string query)
        {
            if (useCaches && caches.ContainsKey(dbType) && caches[dbType].ContainsKey(query))
            {
                return caches[dbType][query];
            }

            string s = QuerySQL((int)dbType, query);

            string[][] ret = JsonConvert.DeserializeObject<string[][]>(QuerySQL((int)dbType, query));

            if (ret.Length > 0)
            {
                if (useCaches)
                {
                    if (caches.ContainsKey(dbType))
                    {
                        caches[dbType].Add(query, ret);
                    }
                    else
                    {
                        Dictionary<string, string[][]> v = new Dictionary<string, string[][]>();

                        v.Add(query, ret);

                        caches.Add(dbType, v);
                    }
                }
            }
            return ret;
        }
        public string[][] querySQL(string projName, string query)
        {
            if (useCaches && caches2.ContainsKey(projName) && caches2[projName].ContainsKey(query))
            {
                return caches2[projName][query];
            }

            if (projDBPath.IndexOf(projName + ".sqlite") != -1)
            {
                return querySQL(type.ProjDB, query);
            }

            if (SecureSQLite.OpenDB("projects\\" + projName + ".sqlite", customDB) == 1)
            {
                string[][] ret = JsonConvert.DeserializeObject<string[][]>(QuerySQL(customDB, query));

                if (useCaches)
                {
                    if (caches2.ContainsKey(projName))
                    {
                        caches2[projName].Add(query, ret);
                    }
                    else
                    {
                        Dictionary<string, string[][]> v = new Dictionary<string, string[][]>();

                        v.Add(query, ret);

                        caches2.Add(projName, v);
                    }
                }
                return ret;
            }
            return new string[0][];
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
            if (exec != "")
            {
                string s = QuerySQL((int)dbType, "SELECT name FROM sqlite_master WHERE type='table' AND name='" + name + "';");
                string[][] ret = JsonConvert.DeserializeObject<string[][]>(QuerySQL((int)dbType, "SELECT name FROM sqlite_master WHERE type='table' AND name='" + name + "';"));

                if (ret.Length > 0)
                {
                    SecureSQLite.ExecuteSQL((int)dbType, exec);
                }
            }
        }

        public void setValue(type dbType, string table, string columns, string values, string key_columns)
        {
            createTable(dbType, table, tables[table]);

            string[] cols = columns.Split(',');
            string[] vals = values.Split(',');
            string[] keys = key_columns.Split(',');

            Program.UTIL.trim(cols);
            Program.UTIL.trim(vals);
            Program.UTIL.trim(keys);

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
                    string[][] ret = JsonConvert.DeserializeObject<string[][]>(QuerySQL((int)dbType, "SELECT * FROM " + table + " WHERE " + cond));

                    if (ret.Length > 0)
                    {
                        condition = cond;
                    }
                }
            }

            if (condition == "")
            {
                SecureSQLite.ExecuteSQL((int)dbType, "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ")");
            }
            else
            {
                int i = -1;
                string upd = "", sql = "UPDATE " + table + " SET ";

                while (++i < cols.Length)
                {
                    if (upd != "") upd += ",";
                    upd += cols[i] + "=" + vals[i];
                }

                sql += upd + " WHERE " + condition;
                SecureSQLite.ExecuteSQL((int)dbType, sql);
            }
        }

        public void deleteTable(type dbType, string table)
        {
            SecureSQLite.ExecuteSQL((int)dbType, "delete from " + table);
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
                        SecureSQLite.ExecuteSQL((int)dbType, "INSERT INTO " + table + " (" + columns + ") SELECT " + columns + " FROM " + table + " WHERE " + conditions + " LIMIT 1");

                        string[][] res1 = JsonConvert.DeserializeObject<string[][]>(QuerySQL((int)type.ProjDB, "SELECT MAX(ID) AS id FROM " + table));

                        SecureSQLite.ExecuteSQL((int)type.ProjDB, "UPDATE " + table + " SET 번호='" + Num + "' WHERE  ID = " + res1[0][0]);

                        return true;
                    }
                }
            }
            return false;
        }
        public void deleteValue(type dbType, string table, string conditions = "")
        {
            string condition = "";

            if (conditions != "")
            {
                SecureSQLite.ExecuteSQL((int)dbType, "delete from " + table + " WHERE " + conditions);
            }
            else
            {
                SecureSQLite.ExecuteSQL((int)dbType, "delete from " + table);
            }
        }

        public string[][] getValue(type dbType, string table, string columns, string conditions = "")
        {
            string sql;

            if (conditions != "")
            {
                sql = "SELECT " + columns + " FROM " + table + " WHERE " + conditions;
            }
            else
            {
                sql = "SELECT " + columns + " FROM " + table;
            }

            if (useCaches && caches.ContainsKey(dbType) && caches[dbType].ContainsKey(sql)) 
            { 
                return caches[dbType][sql];
            }

            string[][] ret = JsonConvert.DeserializeObject<string[][]>(QuerySQL((int)dbType, sql));

            if (useCaches)
            {
                if (caches.ContainsKey(dbType))
                {
                    caches[dbType].Add(sql, ret);
                }
                else
                {
                    Dictionary<string, string[][]> v = new Dictionary<string, string[][]>();

                    v.Add(sql, ret);

                    caches.Add(dbType, v);
                }
            }
            return ret;
        }

        public string[][] getValue(string projName, string table, string columns, string conditions = "")
        {
            string sql;

            if (projDBPath.IndexOf(projName + ".sqlite") != -1)
            {
                return getValue(type.ProjDB, table, columns, conditions);
            }

            if (conditions != "")
            {
                sql = "SELECT " + columns + " FROM " + table + " WHERE " + conditions;
            }
            else
            {
                sql = "SELECT " + columns + " FROM " + table;
            }

            if (useCaches && caches2.ContainsKey(projName) && caches2[projName].ContainsKey(sql))
            {
                return caches2[projName][sql];
            }

            if (SecureSQLite.OpenDB("projects\\" + projName + ".sqlite", customDB) == 1)
            {
                string[][] ret = JsonConvert.DeserializeObject<string[][]>(QuerySQL(customDB, sql));
                if (useCaches)
                {
                    if (caches2.ContainsKey(projName))
                    {
                        caches2[projName].Add(sql, ret);
                    }
                    else
                    {
                        Dictionary<string, string[][]> v = new Dictionary<string, string[][]>();
                        v.Add(sql, ret);
                        caches2.Add(projName, v);
                    }
                }
                return ret;
            }
            return new string[0][];
        }
        //중복 제거하고 값 가져오기
        public string[][] getValue_SameCheck(type dbType, string table, string columns, string conditions = "")
        {
            string sql;
            List<string[]> objects = new List<string[]>();

            if (conditions != "")
            {
                sql = "SELECT DISTINCT " + columns + " FROM " + table + " WHERE " + conditions;
            }
            else
            {
                sql = "SELECT DISTINCT " + columns + " FROM " + table;
            }

            return JsonConvert.DeserializeObject<string[][]>(QuerySQL((int)dbType, sql));
        }
        public string[][] getValue_SameCheck(string projName, string table, string columns, string conditions = "")
        {
            if (projDBPath.IndexOf(projName + ".sqlite") != -1)
            {
                return getValue_SameCheck(type.ProjDB, table, columns, conditions);
            }

            if (SecureSQLite.OpenDB("projects\\" + projName + ".sqlite", customDB) == 1)
            {
                if (conditions != "")
                {
                    return JsonConvert.DeserializeObject<string[][]>(QuerySQL(customDB, "SELECT DISTINCT " + columns + " FROM " + table + " WHERE " + conditions));
                }
                else
                {
                    return JsonConvert.DeserializeObject<string[][]>(QuerySQL(customDB, "SELECT DISTINCT " + columns + " FROM " + table));
                }
            }
            return new string[0][];
        }
        public void UseCaches(bool use)
        {
            useCaches = use;
            caches.Clear();
            caches2.Clear();
        }

        public void UpdateDatabase(string dbPath, string table, string column)
        {
            if (dbPath.IndexOf(projDBPath) != -1)
            {
                MessageBox.Show("현재 실행 중인 프로젝트는 컬럼이 생성되지 않습니다.");
                return;
            }
            else if (SecureSQLite.OpenDB(dbPath, customDB) == 1)
            {
                SecureSQLite.ExecuteSQL((int)customDB, "ALTER TABLE " + table + " ADD COLUMN " + column + " VARCHAR(32);");
                SecureSQLite.SaveDB(dbPath, (int)customDB);
                Console.WriteLine(column + " 컬럼 추가됨: {dbPath}");
                SecureSQLite.CloseDB(customDB);
            }
        }
    }
}



