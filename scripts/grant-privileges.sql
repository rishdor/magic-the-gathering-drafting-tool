REVOKE ALL PRIVILEGES ON ALL TABLES IN SCHEMA public FROM yugi;

GRANT SELECT ON ALL TABLES IN SCHEMA public TO yugi;
-- Allows yugi to validate & create users
GRANT INSERT ON users TO yugi;
--- Allows yugi to use & update the id counter
GRANT SELECT,UPDATE ON users_id_seq TO yugi;

-- Allows yugi to create decks
GRANT INSERT ON user_decks TO yugi;
GRANT INSERT ON deck_cards TO yugi;
--- Alows yugi to use & update the if counter
GRANT SELECT,UPDATE ON user_decks_id_seq TO yugi;
GRANT SELECT,UPDATE ON deck_cards_id_seq TO yugi;