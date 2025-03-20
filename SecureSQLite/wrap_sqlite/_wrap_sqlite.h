#pragma once

#ifdef __cplusplus
extern "C" {
#endif

#define DB_INDEX_MAX		64
#define DB_FIELD_MAX		64

#define DB_NOERR_SQL		0
#define DB_NOERR_OPENED		1
#define DB_NOERR_SAVED		2
#define DB_NOERR_CLOSED		3

#define DB_ERR_INDEX		1000
#define DB_ERR_CORRUPTED	1001
#define DB_ERR_UNSAVED		1002
#define DB_ERR_SQL			1003
#define DB_ERR_OVERFLOWED	1004

#define DB_ROW_COUNT(a) *(gDBResRows+a)
#define DB_COL_COUNT(a) *(gDBResCols+a)
#define DB_COL_NAME(a,c) (*(gDBRes+a))[c]
#define DB_GET_VALUE(a,r,c) (*(gDBRes+a))[r * (*(gDBResCols+a)) + c]

extern char** gDBRes[DB_INDEX_MAX];
extern int		gDBResRows[DB_INDEX_MAX];
extern int		gDBResCols[DB_INDEX_MAX];

extern int db_is_opened(int idx);
extern int db_open(const void* path, int idx);
extern int db_open_mem(int idx);
extern int db_close(int idx);

extern int db_last_changes(int idx);
extern int db_save(const char* path, int idx);

extern int db_query_sql(int idx, const char* sql);
extern int db_query_sql_blob(int idx, const char* sql, char** data, int* len);

extern int db_execute_sql(int idx, const char* sql);
extern int db_execute_sql_blob(int idx, const char* sql, char* bind, int len);

#define INDEX_CHECK	if (idx < 0 || idx > DB_INDEX_MAX) return DB_ERR_INDEX

#ifdef __cplusplus
}  /* end of the 'extern "C"' block */
#endif
