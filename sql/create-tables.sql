CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) UNIQUE,
    cpf VARCHAR(50) UNIQUE,
    full_name VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    type VARCHAR(255) NOT NULL
);

CREATE TABLE wallets (
    id SERIAL PRIMARY KEY,
    balance DECIMAL(10, 2) NOT NULL,
    user_id INT REFERENCES users (id)
);

CREATE TABLE transfers (
    id SERIAL PRIMARY KEY,
    value DECIMAL(10, 2) NOT NULL,
    payer_id INT REFERENCES users (id),
    payee_id INT REFERENCES users (id)
);