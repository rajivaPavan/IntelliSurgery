CREATE DATABASE  IF NOT EXISTS `intellisurgerydb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `intellisurgerydb`;
-- MySQL dump 10.13  Distrib 8.0.26, for Win64 (x86_64)
--
-- Host: localhost    Database: intellisurgerydb
-- ------------------------------------------------------
-- Server version	8.0.26

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20220122173029_Initial','6.0.1'),('20220123050456_ModelsTest1','6.0.1'),('20220124112205_predictedTimeDataType','6.0.1'),('20220124173244_update','6.0.1'),('20220124174508_surgeryevent','6.0.1'),('20220124175628_back','6.0.1'),('20220124181228_change','6.0.1'),('20220124181354_fixedPrevious','6.0.1'),('20220124183037_mapping','6.0.1'),('20220125053538_Events','6.0.1'),('20220125054458_sched','6.0.1'),('20220125055945_keychanges','6.0.1'),('20220125061233_appointmetntupdate','6.0.1'),('20220128052934_major','6.0.1'),('20220128101352_workblock','6.0.1'),('20220128122930_dob','6.0.1'),('20220128191921_init','6.0.1'),('20220129080419_null','6.0.1'),('20220129153053_updatemodels','6.0.1'),('20220130110121_cahnges','6.0.1');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointments`
--

DROP TABLE IF EXISTS `appointments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointments` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `PatientId` int DEFAULT NULL,
  `SurgeonId` int DEFAULT NULL,
  `SurgeryTypeId` int DEFAULT NULL,
  `PriorityLevel` int NOT NULL,
  `AnesthesiaType` int NOT NULL,
  `PredictedTimeDuration` time(6) NOT NULL,
  `Status` int NOT NULL DEFAULT '0',
  `DateAdded` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ScheduledSurgeryId` int DEFAULT NULL,
  `Priority` float DEFAULT NULL,
  `TheatreTypeId` int DEFAULT NULL,
  `TheatreId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Appointments_PatientId` (`PatientId`),
  KEY `IX_Appointments_SurgeonId` (`SurgeonId`),
  KEY `IX_Appointments_SurgeryTypeId` (`SurgeryTypeId`),
  KEY `IX_Appointments_ScheduledSurgeryId` (`ScheduledSurgeryId`),
  KEY `IX_Appointments_DateAdded` (`DateAdded`),
  KEY `IX_Appointments_Priority` (`Priority`),
  KEY `IX_Appointments_PriorityLevel` (`PriorityLevel`),
  KEY `IX_Appointments_Status` (`Status`),
  KEY `IX_Appointments_TheatreTypeId` (`TheatreTypeId`),
  KEY `IX_Appointments_TheatreId` (`TheatreId`),
  CONSTRAINT `FK_Appointments_Patients_PatientId` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`),
  CONSTRAINT `FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId` FOREIGN KEY (`ScheduledSurgeryId`) REFERENCES `scheduledsurgeries` (`Id`),
  CONSTRAINT `FK_Appointments_Surgeons_SurgeonId` FOREIGN KEY (`SurgeonId`) REFERENCES `surgeons` (`Id`),
  CONSTRAINT `FK_Appointments_SurgeryTypes_SurgeryTypeId` FOREIGN KEY (`SurgeryTypeId`) REFERENCES `surgerytypes` (`Id`),
  CONSTRAINT `FK_Appointments_Theatres_TheatreId` FOREIGN KEY (`TheatreId`) REFERENCES `theatres` (`Id`),
  CONSTRAINT `FK_Appointments_TheatreTypes_TheatreTypeId` FOREIGN KEY (`TheatreTypeId`) REFERENCES `theatretypes` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments` VALUES (1,1,1,1,0,0,'00:00:00.000000',6,'2022-01-29 13:37:38.378115',NULL,NULL,NULL,NULL),(2,1,1,1,2,0,'00:00:00.000000',6,'2022-01-29 13:40:42.033542',NULL,NULL,NULL,NULL),(3,1,1,1,1,0,'00:00:00.000000',6,'2022-01-29 14:33:50.915358',NULL,NULL,NULL,NULL),(4,1,1,1,1,0,'00:00:00.000000',6,'2022-01-29 14:36:40.798481',NULL,NULL,NULL,NULL),(5,1,1,1,1,0,'00:00:00.000000',6,'2022-01-29 14:38:15.314922',NULL,NULL,NULL,NULL),(6,1,1,1,1,0,'00:00:00.000000',6,'2022-01-29 15:00:46.398946',NULL,NULL,NULL,NULL),(7,1,1,1,1,0,'00:00:00.000000',6,'2022-01-29 15:02:48.249059',NULL,NULL,NULL,NULL),(8,1,1,1,2,0,'00:00:00.000000',6,'2022-01-29 15:05:39.194836',NULL,NULL,NULL,NULL),(9,1,1,1,0,0,'00:00:00.000000',6,'2022-01-29 15:08:35.379684',NULL,NULL,NULL,NULL),(10,1,1,1,2,0,'00:00:00.000000',6,'2022-01-29 15:15:01.412681',NULL,NULL,NULL,NULL),(11,1,1,1,1,0,'00:00:00.000000',6,'2022-01-29 15:16:23.729603',NULL,NULL,NULL,NULL),(12,1,3,1,1,0,'00:00:00.000000',6,'2022-01-29 15:17:33.723396',NULL,NULL,NULL,NULL),(13,1,1,1,2,0,'00:00:00.000000',6,'2022-01-29 21:21:09.562679',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Gender` int NOT NULL,
  `Weight` float NOT NULL,
  `Height` float NOT NULL DEFAULT '0',
  `DateOfBirth` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES (1,'ABC',0,60,180,'0001-01-01 00:00:00.000000'),(2,'ABC',0,60,180,'0001-01-01 00:00:00.000000');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `scheduledsurgeries`
--

DROP TABLE IF EXISTS `scheduledsurgeries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `scheduledsurgeries` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SurgeryEventId` int DEFAULT NULL,
  `WorkingBlockId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_ScheduledSurgeries_SurgeryEventId` (`SurgeryEventId`),
  KEY `IX_ScheduledSurgeries_WorkingBlockId` (`WorkingBlockId`),
  CONSTRAINT `FK_ScheduledSurgeries_SurgeryEvent_SurgeryEventId` FOREIGN KEY (`SurgeryEventId`) REFERENCES `surgeryevent` (`Id`),
  CONSTRAINT `FK_ScheduledSurgeries_WorkingBlocks_WorkingBlockId` FOREIGN KEY (`WorkingBlockId`) REFERENCES `workingblocks` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scheduledsurgeries`
--

LOCK TABLES `scheduledsurgeries` WRITE;
/*!40000 ALTER TABLE `scheduledsurgeries` DISABLE KEYS */;
/*!40000 ALTER TABLE `scheduledsurgeries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `specialities`
--

DROP TABLE IF EXISTS `specialities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `specialities` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `specialities`
--

LOCK TABLES `specialities` WRITE;
/*!40000 ALTER TABLE `specialities` DISABLE KEYS */;
INSERT INTO `specialities` VALUES (1,'Cardiac Surgeon'),(2,'Orthopedic Surgeon');
/*!40000 ALTER TABLE `specialities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staffworkingperiod`
--

DROP TABLE IF EXISTS `staffworkingperiod`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staffworkingperiod` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SurgeonId` int DEFAULT NULL,
  `Start` datetime(6) NOT NULL,
  `End` datetime(6) NOT NULL,
  `Duration` time(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_StaffWorkingPeriod_SurgeonId` (`SurgeonId`),
  CONSTRAINT `FK_StaffWorkingPeriod_Surgeons_SurgeonId` FOREIGN KEY (`SurgeonId`) REFERENCES `surgeons` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staffworkingperiod`
--

LOCK TABLES `staffworkingperiod` WRITE;
/*!40000 ALTER TABLE `staffworkingperiod` DISABLE KEYS */;
/*!40000 ALTER TABLE `staffworkingperiod` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgeons`
--

DROP TABLE IF EXISTS `surgeons`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgeons` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SpecialityId` int DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_Surgeons_SpecialityId` (`SpecialityId`),
  CONSTRAINT `FK_Surgeons_Specialities_SpecialityId` FOREIGN KEY (`SpecialityId`) REFERENCES `specialities` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgeons`
--

LOCK TABLES `surgeons` WRITE;
/*!40000 ALTER TABLE `surgeons` DISABLE KEYS */;
INSERT INTO `surgeons` VALUES (1,1,'A'),(2,2,'B'),(3,1,'C');
/*!40000 ALTER TABLE `surgeons` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgeryevent`
--

DROP TABLE IF EXISTS `surgeryevent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgeryevent` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Start` datetime(6) NOT NULL,
  `End` datetime(6) NOT NULL,
  `Duration` time(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgeryevent`
--

LOCK TABLES `surgeryevent` WRITE;
/*!40000 ALTER TABLE `surgeryevent` DISABLE KEYS */;
/*!40000 ALTER TABLE `surgeryevent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgerytype_theatres`
--

DROP TABLE IF EXISTS `surgerytype_theatres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgerytype_theatres` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SurgeryTypeId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_SurgeryType_Theatres_SurgeryTypeId` (`SurgeryTypeId`),
  CONSTRAINT `FK_SurgeryType_Theatres_SurgeryTypes_SurgeryTypeId` FOREIGN KEY (`SurgeryTypeId`) REFERENCES `surgerytypes` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgerytype_theatres`
--

LOCK TABLES `surgerytype_theatres` WRITE;
/*!40000 ALTER TABLE `surgerytype_theatres` DISABLE KEYS */;
/*!40000 ALTER TABLE `surgerytype_theatres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `surgerytypes`
--

DROP TABLE IF EXISTS `surgerytypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgerytypes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgerytypes`
--

LOCK TABLES `surgerytypes` WRITE;
/*!40000 ALTER TABLE `surgerytypes` DISABLE KEYS */;
INSERT INTO `surgerytypes` VALUES (1,'Cardiovascular Surgery'),(2,'Neurosurgery');
/*!40000 ALTER TABLE `surgerytypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `theateravailableperiod`
--

DROP TABLE IF EXISTS `theateravailableperiod`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `theateravailableperiod` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TheatreId` int DEFAULT NULL,
  `Start` datetime(6) NOT NULL,
  `End` datetime(6) NOT NULL,
  `Duration` time(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_TheaterAvailablePeriod_TheatreId` (`TheatreId`),
  CONSTRAINT `FK_TheaterAvailablePeriod_Theatres_TheatreId` FOREIGN KEY (`TheatreId`) REFERENCES `theatres` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theateravailableperiod`
--

LOCK TABLES `theateravailableperiod` WRITE;
/*!40000 ALTER TABLE `theateravailableperiod` DISABLE KEYS */;
/*!40000 ALTER TABLE `theateravailableperiod` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `theatres`
--

DROP TABLE IF EXISTS `theatres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `theatres` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TheatreTypeId` int DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_Theatres_TheatreTypeId` (`TheatreTypeId`),
  CONSTRAINT `FK_Theatres_TheatreTypes_TheatreTypeId` FOREIGN KEY (`TheatreTypeId`) REFERENCES `theatretypes` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theatres`
--

LOCK TABLES `theatres` WRITE;
/*!40000 ALTER TABLE `theatres` DISABLE KEYS */;
/*!40000 ALTER TABLE `theatres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `theatretypes`
--

DROP TABLE IF EXISTS `theatretypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `theatretypes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `SurgeryTypeSurgeryTheatreId` int DEFAULT NULL,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_TheatreTypes_SurgeryTypeSurgeryTheatreId` (`SurgeryTypeSurgeryTheatreId`),
  CONSTRAINT `FK_TheatreTypes_SurgeryType_Theatres_SurgeryTypeSurgeryTheatreId` FOREIGN KEY (`SurgeryTypeSurgeryTheatreId`) REFERENCES `surgerytype_theatres` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theatretypes`
--

LOCK TABLES `theatretypes` WRITE;
/*!40000 ALTER TABLE `theatretypes` DISABLE KEYS */;
/*!40000 ALTER TABLE `theatretypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `unscheduledsurgeries`
--

DROP TABLE IF EXISTS `unscheduledsurgeries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `unscheduledsurgeries` (
  `Id` int NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `unscheduledsurgeries`
--

LOCK TABLES `unscheduledsurgeries` WRITE;
/*!40000 ALTER TABLE `unscheduledsurgeries` DISABLE KEYS */;
/*!40000 ALTER TABLE `unscheduledsurgeries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workingblocks`
--

DROP TABLE IF EXISTS `workingblocks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workingblocks` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TheaterAvailablePeriodId` int DEFAULT NULL,
  `SurgeonWorkingPeriodId` int DEFAULT NULL,
  `RemainingTime` time(6) NOT NULL,
  `Start` datetime(6) NOT NULL,
  `End` datetime(6) NOT NULL,
  `Duration` time(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_WorkingBlocks_SurgeonWorkingPeriodId` (`SurgeonWorkingPeriodId`),
  KEY `IX_WorkingBlocks_TheaterAvailablePeriodId` (`TheaterAvailablePeriodId`),
  CONSTRAINT `FK_WorkingBlocks_StaffWorkingPeriod_SurgeonWorkingPeriodId` FOREIGN KEY (`SurgeonWorkingPeriodId`) REFERENCES `staffworkingperiod` (`Id`),
  CONSTRAINT `FK_WorkingBlocks_TheaterAvailablePeriod_TheaterAvailablePeriodId` FOREIGN KEY (`TheaterAvailablePeriodId`) REFERENCES `theateravailableperiod` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `workingblocks`
--

LOCK TABLES `workingblocks` WRITE;
/*!40000 ALTER TABLE `workingblocks` DISABLE KEYS */;
/*!40000 ALTER TABLE `workingblocks` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2022-01-30 18:50:30
