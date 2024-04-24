REVOKE ALL PRIVILEGES ON ALL TABLES IN SCHEMA public FROM yugi;

GRANT SELECT ON ALL TABLES IN SCHEMA public TO yugi;
-- Allows yugi to validate & create users
GRANT INSERT ON users TO yugi;
--- Allows yugi to use & update the id counter
GRANT SELECT,UPDATE ON users_id_seq TO yugi;