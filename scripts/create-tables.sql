DROP TABLE users;
DROP TABLE user_decks;
DROP TABLE deck_cards;
CREATE TABLE users(id SERIAL PRIMARY KEY, username VARCHAR(50) UNIQUE, password VARCHAR(255), email VARCHAR(255) UNIQUE);
CREATE TABLE user_decks(id SERIAL PRIMARY KEY, user_id INT, deck_name VARCHAR(50));
CREATE TABLE deck_cards(id SERIAL PRIMARY KEY, deck_id INT, card_id INT);