USE master
GO

--drop database if it exists
IF DB_ID('capstone_version2') IS NOT NULL
BEGIN
	ALTER DATABASE capstone_version2 SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE capstone_version2;
END

CREATE DATABASE capstone_version2
GO

USE capstone_version2
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
	is_public bit NOT NULL,
	creator_id int NOT NULL,
	CONSTRAINT PK_deck PRIMARY KEY (deck_id),
	CONSTRAINT FK_deck_user FOREIGN KEY (creator_id) REFERENCES users(user_id)
)

CREATE TABLE textcard (
	textcard_id int IDENTITY(1,1) NOT NULL,
	front varchar(100) NOT NULL,
	back varchar(500) NOT NULL,
	card_keywords varchar(200) NOT NULL,
	deck_id int NOT NULL,
	CONSTRAINT PK_textcard PRIMARY KEY (textcard_id),
	CONSTRAINT FK_deck_textcard FOREIGN KEY (deck_id) REFERENCES deck(deck_id)
)

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

INSERT INTO deck (name, description, deck_keywords, is_public, creator_id) VALUES ('Capitals', 'Match U.S. states and capitals', 'state, capital, US', 1, 2);
INSERT INTO deck (name, description, deck_keywords, is_public, creator_id) VALUES ('Software Development', 'Identify basic software development concepts', 'software, computer', 0, 1);

INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is the capital of Ohio?', 'Columbus', 'state, capital, US, Ohio', 1);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is the capital of Missouri?', 'Jefferson City', 'state, capital, US, Missouri', 1);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is the capital of Mississippi?', 'Jackson', 'state, capital, US, Mississippi', 1);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is the capital of Pennsylvania?', 'Harrisburg', 'state, capital, US, Pennsylvania', 1);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is the capital of New York?', 'Albany', 'state, US, New York', 1);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is the capital of Texas?', 'Austin', 'state, US, Texas', 1);

INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('Define polymorphism.', 'something taking many forms', 'software, computer, OOP', 2);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What does DAO stand for?', 'Data Access Object', 'software, computer, DAO, design pattern', 2);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What does MVC stand for?', 'Model, View, Controller', 'software, computer, MVC, design pattern', 2);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('What is a design pattern?', 'a formula to fix a common problem in software development', 'software, computer, design pattern', 2);
INSERT INTO textcard (front, back, card_keywords, deck_id) VALUES ('Give an example of a version control system.', 'git', 'software, computer, version control', 2);

GO