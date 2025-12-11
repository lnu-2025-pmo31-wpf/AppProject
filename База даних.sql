CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(150) UNIQUE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE categories (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    is_expense BOOLEAN NOT NULL
);

CREATE TABLE transactions (
    id SERIAL PRIMARY KEY,
    user_id INT NOT NULL,
    category_id INT NOT NULL,
    amount NUMERIC(12,2) NOT NULL,
    type VARCHAR(20) NOT NULL CHECK(type IN ('income','expense')),
    date DATE NOT NULL,
    note TEXT,

    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

-- Optional tables for family/groups

CREATE TABLE groups (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE user_groups (
    user_id INT NOT NULL,
    group_id INT NOT NULL,
    PRIMARY KEY (user_id, group_id),

    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (group_id) REFERENCES groups(id)
);
