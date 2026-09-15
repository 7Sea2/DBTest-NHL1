-- CREATE DATABASE: Webwinkel

CREATE DATABASE IF NOT EXISTS Webwinkel;
                               USE Webwinkel;

                               SET FOREIGN_KEY_CHECKS = 0;

DROP TABLE IF EXISTS Bestelregels;
DROP TABLE IF EXISTS Bestellingen;
DROP TABLE IF EXISTS Producten;
DROP TABLE IF EXISTS Categorieen;
DROP TABLE IF EXISTS Leveranciers;
DROP TABLE IF EXISTS Klanten;
DROP TABLE IF EXISTS Medewerkers;
DROP TABLE IF EXISTS Verzenddiensten;

SET FOREIGN_KEY_CHECKS = 1;


                               -- TABLE: Leveranciers

CREATE TABLE Leveranciers (
                              LeverancierID INT(11) NOT NULL AUTO_INCREMENT,
                              LeverancierNaam VARCHAR(255) NOT NULL,
                              ContactPersoon VARCHAR(255),
                              Adres VARCHAR(255),
                              Stad VARCHAR(255),
                              Postcode VARCHAR(50),
                              Land VARCHAR(255),
                              Telefoon VARCHAR(255),

                              PRIMARY KEY (LeverancierID)
) ENGINE=InnoDB;


-- TABLE: Categorieen

CREATE TABLE Categorieen (
                             CategorieID INT(11) NOT NULL AUTO_INCREMENT,
                             CategorieNaam VARCHAR(255) NOT NULL,
                             Omschrijving VARCHAR(255),

                             PRIMARY KEY (CategorieID)
) ENGINE=InnoDB;


-- TABLE: Verzenddiensten

CREATE TABLE Verzenddiensten (
                                 VerzenddienstID INT(11) NOT NULL AUTO_INCREMENT,
                                 Naam VARCHAR(255) NOT NULL,
                                 Telefoon VARCHAR(255),

                                 PRIMARY KEY (VerzenddienstID)
) ENGINE=InnoDB;


-- TABLE: Medewerkers

CREATE TABLE Medewerkers (
                             MedewerkerID INT(11) NOT NULL AUTO_INCREMENT,
                             Achternaam VARCHAR(255) NOT NULL,
                             Voornaam VARCHAR(255) NOT NULL,
                             Geboortedatum DATE,
                             Notities TEXT,
                             Foto VARCHAR(50),

                             PRIMARY KEY (MedewerkerID)
) ENGINE=InnoDB;


-- TABLE: Klanten

CREATE TABLE Klanten (
                         KlantID INT(11) NOT NULL AUTO_INCREMENT,
                         KlantNaam VARCHAR(255) NOT NULL,
                         ContactPersoon VARCHAR(255),
                         Adres VARCHAR(255),
                         Stad VARCHAR(255),
                         Postcode VARCHAR(50),
                         Land VARCHAR(255),

                         PRIMARY KEY (KlantID)
) ENGINE=InnoDB;


-- TABLE: Producten

CREATE TABLE Producten (
                           ProductID INT(11) NOT NULL AUTO_INCREMENT,
                           ProductNaam VARCHAR(255) NOT NULL,
                           LeverancierID INT(11),
                           CategorieID INT(11),
                           Eenheid VARCHAR(255),
                           Prijs DECIMAL(10, 2),

                           PRIMARY KEY (ProductID),

                           CONSTRAINT fk_producten_leveranciers
                               FOREIGN KEY (LeverancierID)
                                   REFERENCES Leveranciers (LeverancierID)
                                   ON UPDATE CASCADE
                                   ON DELETE SET NULL,

                           CONSTRAINT fk_producten_categorieen
                               FOREIGN KEY (CategorieID)
                                   REFERENCES Categorieen (CategorieID)
                                   ON UPDATE CASCADE
                                   ON DELETE SET NULL

) ENGINE=InnoDB;


-- TABLE: Bestellingen

CREATE TABLE Bestellingen (
                              BestellingID INT(11) NOT NULL AUTO_INCREMENT,
                              KlantID INT(11),
                              MedewerkerID INT(11),
                              BestelDatum DATE,
                              VerzenddienstID INT(11),

                              PRIMARY KEY (BestellingID),

                              CONSTRAINT fk_bestellingen_klanten
                                  FOREIGN KEY (KlantID)
                                      REFERENCES Klanten (KlantID)
                                      ON UPDATE CASCADE
                                      ON DELETE SET NULL,

                              CONSTRAINT fk_bestellingen_medewerkers
                                  FOREIGN KEY (MedewerkerID)
                                      REFERENCES Medewerkers (MedewerkerID)
                                      ON UPDATE CASCADE
                                      ON DELETE SET NULL,

                              CONSTRAINT fk_bestellingen_verzenddiensten
                                  FOREIGN KEY (VerzenddienstID)
                                      REFERENCES Verzenddiensten (VerzenddienstID)
                                      ON UPDATE CASCADE
                                      ON DELETE SET NULL

) ENGINE=InnoDB;


-- TABLE: Bestelregels

CREATE TABLE Bestelregels (
                              BestelregelID INT(11) NOT NULL AUTO_INCREMENT,
                              BestellingID INT(11) NOT NULL,
                              ProductID INT(11) NOT NULL,
                              Aantal INT(11) NOT NULL,

                              PRIMARY KEY (BestelregelID),

                              CONSTRAINT fk_bestelregels_bestellingen
                                  FOREIGN KEY (BestellingID)
                                      REFERENCES Bestellingen (BestellingID)
                                      ON UPDATE CASCADE
                                      ON DELETE CASCADE,

                              CONSTRAINT fk_bestelregels_producten
                                  FOREIGN KEY (ProductID)
                                      REFERENCES Producten (ProductID)
                                      ON UPDATE CASCADE
                                      ON DELETE RESTRICT

) ENGINE=InnoDB;
                               