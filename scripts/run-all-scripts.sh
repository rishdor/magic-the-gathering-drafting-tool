./run-sql-postgres.sh create-users.sql &&
./run-sql-postgres.sh create-database.sql &&
./run-sql.sh kaiba optimized-mtg.sql &&
./run-sql.sh kaiba create-tables.sql &&
./run-sql.sh kaiba grant-privileges.sql