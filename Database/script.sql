
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

CREATE SCHEMA IF NOT EXISTS `blood-donner` DEFAULT CHARACTER SET utf8 ;

CREATE TABLE IF NOT EXISTS `blood-donner`.`USER` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Fname` NVARCHAR(45) NOT NULL,
  `Lname` NVARCHAR(45) NOT NULL,
  `DOB` DATE NULL DEFAULT NULL,
  `Gender` VARCHAR(45) NULL DEFAULT NULL,
  `Phone` NVARCHAR(45) NOT NULL,
  `Email` NVARCHAR(250) NOT NULL,
  `Password` NVARCHAR(45) NOT NULL,
  `IsDonor` BIT NULL DEFAULT NULL,
  `Role` INT(11) NULL NOT NULL,
  `BloodTypeID` INT(11) NOT NULL,
  `Country` NVARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_USER_BloodType_idx` (`BloodTypeID` ASC),
  CONSTRAINT `fk_USER_BloodType`
    FOREIGN KEY (`BloodTypeID`)
    REFERENCES `blood-donner`.`BloodType` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `blood-donner`.`BloodType` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `Name` NVARCHAR(45) NOT NULL,
  `RareGrade` INT NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `blood-donner`.`BloodRequest` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `RequestDate` DATE NULL DEFAULT NULL,
  `Status` INT(11) NULL DEFAULT NULL,
  `ProoFile` BLOB NULL DEFAULT NULL,
  `BloodTypeID` INT(11) NOT NULL,
  `UserID` INT(11) NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_BloodRequest_BloodType1_idx` (`BloodTypeID` ASC),
  INDEX `fk_BloodRequest_USER1_idx` (`USERID` ASC),
  CONSTRAINT `fk_BloodRequest_BloodType1`
    FOREIGN KEY (`BloodTypeID`)
    REFERENCES `blood-donner`.`BloodType` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_BloodRequest_USER1`
    FOREIGN KEY (`USERID`)
    REFERENCES `blood-donner`.`USER` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `blood-donner`.`BloodDonationHistory` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `DonationDate` DATE NULL DEFAULT NULL,
  `DonorID` INT(11) NOT NULL,
  `RecipientID` INT(11) NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_BloodDonationHistory_USER1_idx` (`DonorID` ASC),
  INDEX `fk_BloodDonationHistory_USER2_idx` (`RecipientID` ASC),
  CONSTRAINT `fk_BloodDonationHistory_USER1`
    FOREIGN KEY (`DonorID`)
    REFERENCES `blood-donner`.`USER` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_BloodDonationHistory_USER2`
    FOREIGN KEY (`RecipientID`)
    REFERENCES `blood-donner`.`USER` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `blood-donner`.`BloodTypeCompatibilty` (
  `BloodTypeID` INT(11) NOT NULL,
  `CompatibleBloodTypeID` INT(11) NOT NULL,
  INDEX `fk_BloodTypeCompatibilty_BloodType1_idx` (`BloodTypeID` ASC),
  INDEX `fk_BloodTypeCompatibilty_BloodType2_idx` (`CompatibleBloodTypeID` ASC),
  PRIMARY KEY (`BloodTypeID`, `CompatibleBloodTypeID`),
  CONSTRAINT `fk_BloodTypeCompatibilty_BloodType1`
    FOREIGN KEY (`BloodTypeID`)
    REFERENCES `blood-donner`.`BloodType` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_BloodTypeCompatibilty_BloodType2`
    FOREIGN KEY (`CompatibleBloodTypeID`)
    REFERENCES `blood-donner`.`BloodType` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `blood-donner`.`City` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `CountryName` NVARCHAR(50) NOT NULL,
  `CountryId` INT(11) NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX `fk_City_Country_idx` (`CountryId` ASC),
  CONSTRAINT `fk_City_Country`
    FOREIGN KEY (`CountryId`)
    REFERENCES `blood-donner`.`Country` (`ID`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

CREATE TABLE IF NOT EXISTS `blood-donner`.`Country` (
  `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `CountryName` NVARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
