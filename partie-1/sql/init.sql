-- ============================================
--  BASE DE DONNÉES - GESTION DE BIBLIOTHÈQUE
-- ============================================

CREATE DATABASE IF NOT EXISTS bibliotheque
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE bibliotheque;

-- ============================================
--  TABLE : genres
-- ============================================
CREATE TABLE genres (
    id_genre    INT UNSIGNED    AUTO_INCREMENT PRIMARY KEY,
    nom         VARCHAR(100)    NOT NULL UNIQUE
);

-- ============================================
--  TABLE : rayons
-- ============================================
CREATE TABLE rayons (
    id_rayon    INT UNSIGNED    AUTO_INCREMENT PRIMARY KEY,
    nom         VARCHAR(100)    NOT NULL UNIQUE
);

-- ============================================
--  TABLE : etageres
-- ============================================
CREATE TABLE etageres (
    id_etagere  INT UNSIGNED    AUTO_INCREMENT PRIMARY KEY,
    nom         VARCHAR(50)     NOT NULL,
    id_rayon    INT UNSIGNED    NOT NULL,
    CONSTRAINT fk_etagere_rayon FOREIGN KEY (id_rayon)
        REFERENCES rayons(id_rayon)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    UNIQUE KEY uq_etagere_rayon (nom, id_rayon)
);

-- ============================================
--  TABLE : livres
-- ============================================
CREATE TABLE livres (
    id_livre            INT UNSIGNED        AUTO_INCREMENT PRIMARY KEY,
    titre               VARCHAR(255)        NOT NULL,
    auteur              VARCHAR(255)        NOT NULL,
    isbn                VARCHAR(20)         NOT NULL UNIQUE,
    annee_publication   YEAR                NOT NULL,
    id_genre            INT UNSIGNED        NOT NULL,
    id_rayon            INT UNSIGNED        NOT NULL,
    id_etagere          INT UNSIGNED        NOT NULL,
    disponible          TINYINT(1)          NOT NULL DEFAULT 1,
    couverture          LONGBLOB            NULL,
    date_ajout          DATETIME            NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_livre_genre   FOREIGN KEY (id_genre)
        REFERENCES genres(id_genre)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_livre_rayon   FOREIGN KEY (id_rayon)
        REFERENCES rayons(id_rayon)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_livre_etagere FOREIGN KEY (id_etagere)
        REFERENCES etageres(id_etagere)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
--  TABLE : adherents
-- ============================================
CREATE TABLE adherents (
    id_adherent INT UNSIGNED    AUTO_INCREMENT PRIMARY KEY,
    nom         VARCHAR(100)    NOT NULL,
    prenom      VARCHAR(100)    NOT NULL,
    email       VARCHAR(255)    NOT NULL UNIQUE,
    telephone   VARCHAR(20)     NULL,
    date_inscription DATETIME   NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- ============================================
--  TABLE : emprunts
-- ============================================
CREATE TABLE emprunts (
    id_emprunt      INT UNSIGNED    AUTO_INCREMENT PRIMARY KEY,
    id_livre        INT UNSIGNED    NOT NULL,
    id_adherent     INT UNSIGNED    NOT NULL,
    date_emprunt    DATE            NOT NULL DEFAULT (CURRENT_DATE),
    date_retour_prevue DATE         NOT NULL,
    date_retour_reelle DATE         NULL,
    CONSTRAINT fk_emprunt_livre     FOREIGN KEY (id_livre)
        REFERENCES livres(id_livre)
        ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT fk_emprunt_adherent  FOREIGN KEY (id_adherent)
        REFERENCES adherents(id_adherent)
        ON DELETE RESTRICT ON UPDATE CASCADE
);

-- ============================================
--  TRIGGER : MAJ disponibilité lors d'un emprunt
-- ============================================
DELIMITER $$

CREATE TRIGGER trg_emprunt_after_insert
AFTER INSERT ON emprunts
FOR EACH ROW
BEGIN
    UPDATE livres SET disponible = 0 WHERE id_livre = NEW.id_livre;
END$$

CREATE TRIGGER trg_retour_after_update
AFTER UPDATE ON emprunts
FOR EACH ROW
BEGIN
    IF NEW.date_retour_reelle IS NOT NULL AND OLD.date_retour_reelle IS NULL THEN
        UPDATE livres SET disponible = 1 WHERE id_livre = NEW.id_livre;
    END IF;
END$$

DELIMITER ;

-- ============================================
--  INDEX pour la recherche (perf)
-- ============================================
CREATE INDEX idx_livre_titre   ON livres(titre);
CREATE INDEX idx_livre_auteur  ON livres(auteur);
CREATE INDEX idx_livre_isbn    ON livres(isbn);
CREATE INDEX idx_livre_genre   ON livres(id_genre);

-- ============================================
--  DONNÉES DE TEST
-- ============================================
INSERT INTO genres (nom) VALUES
    ('Roman'), ('Science-Fiction'), ('Policier'),
    ('Histoire'), ('Biographie'), ('Jeunesse'), ('Informatique');

INSERT INTO rayons (nom) VALUES
    ('Rayon A'), ('Rayon B'), ('Rayon C');

INSERT INTO etageres (nom, id_rayon) VALUES
    ('Etagere 1', 1), ('Etagere 2', 1),
    ('Etagere 1', 2), ('Etagere 2', 2),
    ('Etagere 1', 3);

INSERT INTO livres (titre, auteur, isbn, annee_publication, id_genre, id_rayon, id_etagere) VALUES
    ('Le Petit Prince',          'Antoine de Saint-Exupery', '9782070612758', 1943, 6, 1, 1),
    ('1984',                     'George Orwell',            '9782070368228', 1949, 2, 1, 2),
    ('Dune',                     'Frank Herbert',            '9782221257074', 1965, 2, 2, 3),
    ('Le Da Vinci Code',         'Dan Brown',                '9782709626091', 2003, 3, 2, 4),
    ('Sapiens',                  'Yuval Noah Harari',        '9782226257017', 2011, 4, 3, 5),
    ('Clean Code',               'Robert C. Martin',         '9780132350884', 2008, 7, 3, 5);

INSERT INTO adherents (nom, prenom, email, telephone) VALUES
    ('Dupont', 'Marie',   'marie.dupont@email.com',  '0601020304'),
    ('Martin', 'Lucas',   'lucas.martin@email.com',  '0605060708');