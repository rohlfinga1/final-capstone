USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE deck (
	deck_id int IDENTITY(1,1) NOT NULL,
	name varchar(50) NOT NULL,
	description varchar(200) NOT NULL,
	deck_keywords varchar(200) NOT NULL,
	date_made date NOT NULL,
	creator_id int NOT NULL,
	is_public bit NOT NULL,
	CONSTRAINT PK_deck PRIMARY KEY (deck_id),
	CONSTRAINT FK_deck_users FOREIGN KEY (creator_id) REFERENCES users(user_id)
)

CREATE TABLE textcard (
	textcard_id int IDENTITY(1,1) NOT NULL,
	front varchar(100) NOT NULL,
	back varchar(500) NOT NULL,
	card_keywords varchar(200) NOT NULL,
	date_made date NOT NULL,
	creator_id int NOT NULL,
	CONSTRAINT PK_textcard PRIMARY KEY (textcard_id),
	CONSTRAINT FK_textcard_users FOREIGN KEY (creator_id) REFERENCES users(user_id)
)

CREATE TABLE deck_textcard (
	deck_id int NOT NULL,
	textcard_id int NOT NULL,
	CONSTRAINT FK_deck_deck_textcard FOREIGN KEY (deck_id) REFERENCES deck(deck_id),
	CONSTRAINT FK_textcard_deck_textcard FOREIGN KEY (textcard_id) REFERENCES textcard(textcard_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO deck (name, description, deck_keywords, date_made, creator_id, is_public) VALUES ('Capitals', 'Match U.S. states and capitals', 'state, capital, US', '2022-12-10', 2, 1);
INSERT INTO deck (name, description, deck_keywords, date_made, creator_id, is_public) VALUES ('Software Development', 'Identify basic software development concepts', 'software, computer', '2022-12-10', 1, 0);

INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is the capital of Ohio?', 'Columbus', 'state, capital, US, Ohio', '2022-12-10', 2);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is the capital of Missouri?', 'Jefferson City', 'state, capital, US, Missouri', '2022-12-10', 2);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is the capital of Mississippi?', 'Jackson', 'state, capital, US, Mississippi', '2022-12-10', 2);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is the capital of Pennsylvania?', 'Harrisburg', 'state, capital, US, Pennsylvania', '2022-12-10', 2);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is the capital of New York?', 'Albany', 'state, US, New York', '2022-12-10', 2);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is the capital of Texas?', 'Austin', 'state, US, Texas', '2022-12-10', 2);

INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('Define polymorphism.', 'something taking many forms', 'software, computer, OOP', '2022-12-10', 1);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What does DAO stand for?', 'Data Access Object', 'software, computer, DAO, design pattern', '2022-12-10', 1);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What does MVC stand for?', 'Model, View, Controller', 'software, computer, MVC, design pattern', '2022-12-10', 1);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('What is a design pattern?', 'a formula to fix a common problem in software development', 'software, computer, design pattern', '2022-12-10', 1);
INSERT INTO textcard (front, back, card_keywords, date_made, creator_id) VALUES ('Give an example of a version control system.', 'git', 'software, computer, version control', '2022-12-10', 1);

INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (1, 1);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (1, 2);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (1, 3);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (1, 4);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (1, 5);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (1, 6);

INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (2, 7);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (2, 8);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (2, 9);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (2, 10);
INSERT INTO deck_textcard (deck_id, textcard_id) VALUES (2, 11);

GO