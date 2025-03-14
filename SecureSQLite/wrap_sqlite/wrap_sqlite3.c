

#include <stdio.h>
#include <stdlib.h>
#include "_wrap_sqlite.h"

#include "sqlite3.h"

int			first = 1;
sqlite3 *	gDBs[DB_INDEX_MAX];
char **		gDBRes[DB_INDEX_MAX];
int			gDBResRows[DB_INDEX_MAX];
int			gDBResCols[DB_INDEX_MAX];

/*
** This function is used to load the contents of a database file on disk 
** into the "main" database of open database connection pInMemory, or
** to save the current contents of the database opened by pInMemory into
** a database file on disk. pInMemory is probably an in-memory database, 
** but this function will also work fine if it is not.
**
** Parameter zFilename points to a nul-terminated string containing the
** name of the database file on disk to load from or save to. If parameter
** isSave is non-zero, then the contents of the file zFilename are 
** overwritten with the contents of the database opened by pInMemory. If
** parameter isSave is zero, then the contents of the database opened by
** pInMemory are replaced by data loaded from the file zFilename.
**
** If the operation is successful, SQLITE_OK is returned. Otherwise, if
** an error occurs, an SQLite error code is returned.
*/
static int loadOrSaveDb(sqlite3 *pInMemory, const char *zFilename, int isSave){
  int rc;                   /* Function return code */
  sqlite3 *pFile;           /* Database connection opened on zFilename */
  sqlite3_backup *pBackup;  /* Backup object used to copy data */
  sqlite3 *pTo;             /* Database to copy to (pFile or pInMemory) */
  sqlite3 *pFrom;           /* Database to copy from (pFile or pInMemory) */

  /* Open the database file identified by zFilename. Exit early if this fails
  ** for any reason. */
  rc = sqlite3_open16(zFilename, &pFile);
  if( rc==SQLITE_OK ){

    /* If this is a 'load' operation (isSave==0), then data is copied
    ** from the database file just opened to database pInMemory. 
    ** Otherwise, if this is a 'save' operation (isSave==1), then data
    ** is copied from pInMemory to pFile.  Set the variables pFrom and
    ** pTo accordingly. */
    pFrom = (isSave ? pInMemory : pFile);
    pTo   = (isSave ? pFile     : pInMemory);

    /* Set up the backup procedure to copy from the "main" database of 
    ** connection pFile to the main database of connection pInMemory.
    ** If something goes wrong, pBackup will be set to NULL and an error
    ** code and  message left in connection pTo.
    **
    ** If the backup object is successfully created, call backup_step()
    ** to copy data from pFile to pInMemory. Then call backup_finish()
    ** to release resources associated with the pBackup object.  If an
    ** error occurred, then  an error code and message will be left in
    ** connection pTo. If no error occurred, then the error code belonging
    ** to pTo is set to SQLITE_OK.
    */
    pBackup = sqlite3_backup_init(pTo, "main", pFrom, "main");
    if( pBackup ){
      (void)sqlite3_backup_step(pBackup, -1);
      (void)sqlite3_backup_finish(pBackup);
    }
    rc = sqlite3_errcode(pTo);
  }

  /* Close the database connection opened on database file zFilename
  ** and return the result of this function. */
  (void)sqlite3_close(pFile);
  return rc;
}

int db_is_opened(int idx)
{
	return (gDBs[idx] ? 1 : 0);
}

int db_open2(void * path, int idx)
{
	if (first) { // if it is the first call, then reset db channels.
		first = 0;
		memset(gDBs, 0, sizeof(gDBs));
		memset(gDBRes, 0, sizeof(gDBRes));
	}

	INDEX_CHECK;

	if (gDBs[idx]) sqlite3_close(gDBs[idx]);
	
	if (sqlite3_open(":memory:", &gDBs[idx]) == SQLITE_OK) {
		char* sql = "create table requests (num INTEGER PRIMARY KEY AUTOINCREMENT, data VARCHAR(1024));"
			"create table sending (wid INTEGER PRIMARY KEY, uid_list VARCHAR(255), uids_list VARCHAR(255), login_list VARCHAR(255), data VARCHAR(1024)); "
			"create table version (version char(32));"
			"create table working (num INTEGER PRIMARY KEY AUTOINCREMENT DEFAULT 1, wid INTEGER DEFAULT (-1), uid INTEGER DEFAULT 1, uids CHAR(32) DEFAULT __ruler__, vnet_id CHAR(255), url CHAR(255) DEFAULT _, pwd CHAR(32), path CHAR(255), aid INTEGER DEFAULT 0, name VARCHAR2(128), regdate DATETIME DEFAULT(strftime('%Y-%m-%d %H:%M:%f', 'now', 'localtime')), region VARCHAR2(255), state SMALLINT DEFAULT 1, active BOOL DEFAULT 0, setting TEXT, explain VARCHAR2(255), ext CHAR(8)); "
			"create UNIQUE INDEX [key_working] ON [working] ([num], [wid], [uid], [uids], [vnet_id], [url]);"
			"CREATE TABLE [netffice] ([num] INTEGER PRIMARY KEY AUTOINCREMENT DEFAULT 1, [use] INTEGER DEFAULT 0, [wnum] INTEGER CONSTRAINT[foreign_key_wnum] REFERENCES[working]([num]) ON DELETE CASCADE DEFAULT 1); "
			"CREATE UNIQUE INDEX [index_unique_num] ON [netffice] ([wnum]);PRAGMA synchronous=OFF;PRAGMA journal_mode=OFF;";

		return (sqlite3_exec(gDBs[idx], sql, 0, 0, 0) != SQLITE_OK ? DB_ERR_CORRUPTED : DB_NOERR_OPENED);
	}
	return DB_ERR_CORRUPTED;
}

int db_open(void * path, int idx)
{
	if (first) { // if it is the first call, then reset db channels.
		first = 0;
		memset(gDBs, 0, sizeof(gDBs));
		memset(gDBRes, 0, sizeof(gDBRes));
	}

	INDEX_CHECK;

	if (gDBs[idx]) sqlite3_close(gDBs[idx]);

	if (sqlite3_open(":memory:", &gDBs[idx]) == SQLITE_OK && loadOrSaveDb(gDBs[idx], path, 0) == SQLITE_OK) {
		sqlite3_exec(gDBs[idx], "PRAGMA synchronous=OFF", 0, 0, 0 );
		sqlite3_exec(gDBs[idx], "PRAGMA journal_mode=OFF;", 0, 0, 0 );
		return DB_NOERR_OPENED;
	}
	return DB_ERR_CORRUPTED;
}

int db_save(void * path, int idx)
{
	return loadOrSaveDb(gDBs[idx], path, 1);
}

int db_close(int idx)
{
	INDEX_CHECK;

	if (gDBRes[idx]) {
		sqlite3_free_table(gDBRes[idx]);
		gDBRes[idx] = 0;
	}

	if (gDBs[idx]) {
		sqlite3_close(gDBs[idx]);
		gDBs[idx] = 0;
	}
	return DB_NOERR_CLOSED;
}

int db_query_sql(int idx, char * sql)
{
	INDEX_CHECK;

	if (gDBRes[idx]) {
		sqlite3_free_table(gDBRes[idx]);
		gDBRes[idx] = 0;
	}

	char ** retStrings = 0;
	int iRows = 0, iCols = 0;

	sqlite3_get_table(gDBs[idx], sql, &retStrings, &iRows, &iCols, 0);

	if (iRows > 0 && iCols > 0) {
		gDBRes[idx] = retStrings;
		gDBResRows[idx] = iRows;
		gDBResCols[idx] = iCols;

		return DB_NOERR_SQL;
	}
	else {
		if (gDBRes[idx]) {
			sqlite3_free_table(gDBRes[idx]);
			gDBRes[idx] = 0;
		}
		return DB_ERR_SQL;
	}
}

int db_last_changes(int idx)
{
	INDEX_CHECK;

	return sqlite3_changes(gDBs[idx]);
}

int db_execute_sql(int idx, char * sql)
{
	INDEX_CHECK;

	return !sqlite3_exec(gDBs[idx], sql, 0, 0, 0) ? DB_NOERR_SQL : DB_ERR_SQL;
}

// select field is only one.
int db_query_sql_blob(int idx, char * sql, char ** data, int * len)
{
	int ret = DB_ERR_SQL;
	sqlite3_stmt *pStmt;

	INDEX_CHECK;

	if (sqlite3_prepare_v2(gDBs[idx],  sql, -1, &pStmt, 0) == SQLITE_OK) {
		if (sqlite3_step(pStmt) == SQLITE_ROW) {
			*len = sqlite3_column_bytes(pStmt, 0);
			*data = (char *)malloc(*len);
			memcpy(*data, sqlite3_column_blob(pStmt, 0), *len);
			ret = DB_NOERR_SQL;
		}
		sqlite3_finalize(pStmt);
	}
	return ret;
}

// insert or update field is only one.
int db_execute_sql_blob(int idx, char * sql, char * bind, int len)
{
	sqlite3_stmt *pStmt;

	INDEX_CHECK;

	if (sqlite3_prepare_v2(gDBs[idx],  sql, -1, &pStmt, 0) == SQLITE_OK) {
		sqlite3_bind_blob(pStmt, 1, bind, len, SQLITE_STATIC);
		sqlite3_step(pStmt);
		sqlite3_finalize(pStmt);
		return DB_NOERR_SQL;
	}
	return DB_ERR_SQL;
}
