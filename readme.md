
1. SW 컴포넌트 구성도

![제목 없는 다이어그램 drawio](https://user-images.githubusercontent.com/128672029/234760868-7a1a0023-c229-4baa-9ae3-8911a076e078.png)

2. 작업환경 설정 방법

  (1) 이 소스를 클론한다.
  
      폴더 구조는 아래와 같다.
      
![image](https://user-images.githubusercontent.com/128672029/234761654-84d4cce9-045e-48ad-871e-3e33603d053b.png)
  
  (2) https://www.fitterlite.com/apps/si/asset.zip 를 다운로드하여 압축해제한 후 asset 폴더를 클론한 같은 이름의 폴더에 덮어 씌운다.
  
  (3) https://www.fitterlite.com/apps/si/sw.zip 를 다운로드하여 압축해제한 후 main.exe 를 실행하여 각종 컴포넌트를 설치한다. 
  
      이때 실행후 3차원 박스 애니메이션 창이 실행되는지까지 확인한다.

  (4) 비주얼 스튜디오 2022 커뮤니티 버전(64비트)을 PC 에 설치하고 main.sln 을 로드한다.
  
  (5) main C# 프로젝트에서 작업한다.
  
      개발은 Debug 모드에서 하시기 바랍니다. Release 모드는 제품화 코드가 추가되어 디버깅이 용이하지 않을것 같습니다. 

3. 기초 데이터의 데이터베이스화 작업 단계

  (1) 원본 엑셀 파일은 기초 데이터 폴더의 엑셀 파일들과 같이 하나의 파일이 하나의 데이터베이스가 되는 형태이다. 기초 데이터 폴더의 엑셀 파일들을 참조하여 유사한 로우, 컬럼 구조로 원본 엑셀 파일을 편집한다. 엑셀 파일 내 시트의 개수는 한개이다.
  
  (2) 원본 엑셀 파일을 탭으로 분리 (txt) 파일로 저장한다.
  
  (3) 탭으로 분리 파일의 인코딩을 UTF-8 로 변환한다.(노트패드 기능 활용할것)
  
  ![image](https://user-images.githubusercontent.com/128672029/235830326-6c964403-d5cc-4e04-ade3-f6cdbac4b789.png)
  
  (4) dbmaker 프로그램을 실행시키고 아래 이미지를 참조하여 데이터베이스 변환한다.
  
  ![image](https://user-images.githubusercontent.com/128672029/235831351-2c2b1317-4b24-4211-88a2-1cb422cff29b.png)

4. DB API 의 종류와 활용법

   (1) DB 셋
   
   기초 DB, 프로젝트 DB, 계산 DB 로 구성되며, 프로젝트 DB, 계산 DB는 프로젝트별로 생긴다. 이때 계산 DB 는 메모리 DB 로 프로그램이 종료되면 사라진다.
   
   어느 폼에서든 DB.type.BaseDB(기초 DB), DB.type.ProjDB(프로젝트 DB), DB.type.CalcDB(계산 DB) 열거형 상수로 사용한다.

   (2) DB API 의 종류
   
   openDB(프로젝트명) - 어느 폼에서든 Program.DB.openDB(프로젝트명); 을 실행하면 신규 DB 셋을 열수 있다. 이때 사용중인 DB 는 자동 종료된다.
   
   executeSQL(DB.type, 등록수정SQL) - 어느 폼에서든 Program.DB.executeSQL(DB.type, 등록수정SQL); 를 실행하여 DB 셋 내의 특정 DB 를 수정할수 있다. 
   
   querySQL(DB.type, 조회SQL) - 어느 폼에서든 Program.DB.querySQL(DB.type, 조회SQL); 를 실행하여 DB 셋 내의 특정 DB 의 레코드셋을 가져온다. 이때 레코드셋은 string[][] 형식이다.
   
   setValue(DB.type, 테이블명, 컬럼리스트(,로구분), 값리스트(,로구분), 키리스트(,로구분)) - 어느 폼에서든 Program.DB.setValue(DB.type, 테이블명, 컬럼리스트, 값리스트, 키리스트); 를 실행하여 DB 셋 내의 특정 테이블의 레코드셋을  등록 수정한다. 이때 등록 또는 수정여부는 키리스트의 레코드를 참조하여 자동으로 결정한다. 이 함수는 기초DB 에 대한 수정을 금지한다.
   
   getValue(DB.type, 테이블명, 컬럼리스트(,로구분), 조건문) - 어느 폼에서든 Program.DB.getValue(DB.type, 테이블명, 컬럼리스트, 조건문); 를 실행하여 DB 셋 내의 특정 테이블의 조건에 맞는 값을 string[][] 형식으로 리턴한다.
   
   (3) DB API 의 사용준비
   
   DB API 는 테이블을 자동 생성하는데, 이때 사용할 테이블 생성 SQL 문을 딕셔너리에 추가해야 한다.
   DB.cs 소스파일 내에  
   
   private Dictionary<string, string> tables
   
   딕셔너리에 추가한다. 이때의 키는 테이블명이다.
   
5. CALC(계산 관리 모듈) API 의 활용법    

   CALC API 는 아래 한가지이다.
   
   run(시나리오(string[] 형식의 문자열 배열));
   
   어느 폼에서든 Program.CALC.run(new string[] {"시나리오1", "시나리오2" }); 와 같은 형태로 사용한다.
   
   이때 사용되는 시나리오는 CALC.cs 모듈에 등록해야 한다.
   
   시나리오의 등록은 아래 절차를 따른다.
   
   (1) CALC.cs 의 init 함수 내에  
   
       시나리오함수 라는 실행 함수가 있다면

	   _calculations[시나리오명] = new Func<bool>(시나리오함수);
	   
	   와 같이 등록한다.
	   
   (2) 시나리오 함수는 
	   
	   private static bool 시나리오함수()

       와 같은 형식이고, CALC.cs 내에 선언한다.
	   
   이때 시나리오 함수는 여러 함수에 걸친 변수가 필요한 경우 CalcDB 를 활용하여 변수값 전달을 한다.
   
6. 유틸리티 기능 (UTIL.cs)

   (1) 콤보박스의 DB 인덱스 테이블 바인딩 함수
   
       BaseDB 내의 인덱스 테이블은 인덱스, 인덱스분류 의 두가지이다.
![image](https://github.com/korat1221/Remodeling_SW/assets/128672029/0d840386-65e3-4ea8-9e6a-4b77e686ee18)
       <인덱스 테이블>
       
![image](https://github.com/korat1221/Remodeling_SW/assets/128672029/474cdd71-4ffb-4acf-8026-9edc60abc497)
       <인덱스분류 테이블>
       
       즉, 프로그램에서 사용되는 모든 인덱스는 인덱스 테이블에 모여있고, 그 인덱스 레코드의 분류 관련 데이터는 인덱스분류에 모여있다.
       인덱스 테이블 바인딩 함수는 이 두 테이블을 이용한다.
       
       1) FillComboBox_ByCategory(콤보박스, 인덱스분류의 종류, 인덱스분류의 이름, 기본값)

          어느 폼에서든 Program.UTIL.FillComboBox_ByCategory(comboBox2, "커튼월", "프레임도어", "3"); 와 같이 실행하여 콤보박스에 인덱스 테이블 데이터를 바인딩한다. 이 함수는 다른 콤보박스와의 연관성이 없는 단일 콤보박스나, 다른 콤보박스에 영향을 미치는 대분류 콤보박스에 사용한다.
	  
       2) FillComboBox_ByComboBox(대상 콤보박스, 소스 콤보박스, 기본값)

	  한 콤보박스에 FillComboBox_ByCategory 함수를 이용하여 값이 채워져 있다면, 초기화 또는 콤보박스 선택 핸들러 함수 내에서 이 소스 콤보 박스의 선택값을 이용하여 자식 콤보박스의 값을 채운다.
	  
        사용 예시는 FormDebug.cs 파일을 참조한다.	  
            

       
   
