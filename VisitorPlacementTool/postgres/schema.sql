CREATE TABLE IF NOT EXISTS visitors (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL UNIQUE,
    birthday TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS groups (
    id serial PRIMARY KEY,
    members varchar[],
    event_id int,
    registry_datetime TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS individual_registrations (
    id serial PRIMARY KEY,
    visitor varchar,
    event_id int,
    registry_datetime TIMESTAMP WITH TIME ZONE NOT NULL
);

CREATE TABLE IF NOT EXISTS events (
    id serial PRIMARY KEY,
    name varchar (255) NOT NULL,
    max_visitors int not null,
    creation_datetime TIMESTAMP WITH TIME ZONE NOT NULL
);