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
INSERT INTO `__efmigrationshistory` VALUES ('20220122173029_Initial','6.0.1'),('20220123050456_ModelsTest1','6.0.1'),('20220124112205_predictedTimeDataType','6.0.1'),('20220124173244_update','6.0.1'),('20220124174508_surgeryevent','6.0.1'),('20220124175628_back','6.0.1'),('20220124181228_change','6.0.1'),('20220124181354_fixedPrevious','6.0.1'),('20220124183037_mapping','6.0.1'),('20220125053538_Events','6.0.1'),('20220125054458_sched','6.0.1'),('20220125055945_keychanges','6.0.1'),('20220125061233_appointmetntupdate','6.0.1'),('20220128052934_major','6.0.1'),('20220128101352_workblock','6.0.1'),('20220128122930_dob','6.0.1'),('20220128191921_init','6.0.1'),('20220129080419_null','6.0.1'),('20220129153053_updatemodels','6.0.1'),('20220130110121_cahnges','6.0.1'),('20220131160357_many2many','6.0.1'),('20220131184057_AppointmentForeignKeys','6.0.1'),('20220131184706_AppointmentCompositeIndexes','6.0.1'),('20220131193811_PatientNewCols','6.0.1'),('20220131194826_appointmenyNewColsupdate','6.0.1'),('20220201080810_app-complication','6.0.1'),('20220201122432_upd','6.0.1'),('20220202044534_surgerystaffchange','6.0.1');
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
  `PatientId` int NOT NULL DEFAULT '0',
  `SurgeonId` int NOT NULL DEFAULT '0',
  `SurgeryTypeId` int NOT NULL DEFAULT '0',
  `PriorityLevel` int NOT NULL,
  `AnesthesiaType` int NOT NULL,
  `SystemPredictedDuration` time(6) NOT NULL,
  `Status` int NOT NULL DEFAULT '0',
  `DateAdded` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `ScheduledSurgeryId` int DEFAULT NULL,
  `Priority` float DEFAULT NULL,
  `TheatreTypeId` int NOT NULL DEFAULT '0',
  `TheatreId` int DEFAULT NULL,
  `ComplicationPossibility` tinyint(1) NOT NULL DEFAULT '0',
  `ApproximateProcedureDate` datetime(6) DEFAULT NULL,
  `SurgeonsPredictedDuration` time(6) DEFAULT NULL,
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
  KEY `IX_Appointments_ScheduledSurgeryId_SurgeonId` (`ScheduledSurgeryId`,`SurgeonId`),
  KEY `IX_Appointments_ScheduledSurgeryId_SurgeryTypeId` (`ScheduledSurgeryId`,`SurgeryTypeId`),
  KEY `IX_Appointments_ScheduledSurgeryId_TheatreId` (`ScheduledSurgeryId`,`TheatreId`),
  KEY `IX_Appointments_ScheduledSurgeryId_TheatreTypeId` (`ScheduledSurgeryId`,`TheatreTypeId`),
  CONSTRAINT `FK_Appointments_Patients_PatientId` FOREIGN KEY (`PatientId`) REFERENCES `patients` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_ScheduledSurgeries_ScheduledSurgeryId` FOREIGN KEY (`ScheduledSurgeryId`) REFERENCES `scheduledsurgeries` (`Id`),
  CONSTRAINT `FK_Appointments_Surgeons_SurgeonId` FOREIGN KEY (`SurgeonId`) REFERENCES `surgeons` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_SurgeryTypes_SurgeryTypeId` FOREIGN KEY (`SurgeryTypeId`) REFERENCES `surgerytypes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_Theatres_TheatreId` FOREIGN KEY (`TheatreId`) REFERENCES `theatres` (`Id`),
  CONSTRAINT `FK_Appointments_TheatreTypes_TheatreTypeId` FOREIGN KEY (`TheatreTypeId`) REFERENCES `theatretypes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `disease`
--

DROP TABLE IF EXISTS `disease`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `disease` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `DiseaseEnum` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `disease`
--

LOCK TABLES `disease` WRITE;
/*!40000 ALTER TABLE `disease` DISABLE KEYS */;
INSERT INTO `disease` VALUES (1,1),(2,2),(3,3),(4,4),(5,5),(6,6),(7,7),(8,8);
/*!40000 ALTER TABLE `disease` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `diseasepatient`
--

DROP TABLE IF EXISTS `diseasepatient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `diseasepatient` (
  `DiseasesId` int NOT NULL,
  `PatientsId` int NOT NULL,
  PRIMARY KEY (`DiseasesId`,`PatientsId`),
  KEY `IX_DiseasePatient_PatientsId` (`PatientsId`),
  CONSTRAINT `FK_DiseasePatient_Disease_DiseasesId` FOREIGN KEY (`DiseasesId`) REFERENCES `disease` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_DiseasePatient_Patients_PatientsId` FOREIGN KEY (`PatientsId`) REFERENCES `patients` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `diseasepatient`
--

LOCK TABLES `diseasepatient` WRITE;
/*!40000 ALTER TABLE `diseasepatient` DISABLE KEYS */;
/*!40000 ALTER TABLE `diseasepatient` ENABLE KEYS */;
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
  `ASA_Status` int NOT NULL DEFAULT '0',
  `BMI` float NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
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
  `TheatreId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_StaffWorkingPeriod_SurgeonId` (`SurgeonId`),
  KEY `IX_StaffWorkingPeriod_TheatreId` (`TheatreId`),
  CONSTRAINT `FK_StaffWorkingPeriod_Surgeons_SurgeonId` FOREIGN KEY (`SurgeonId`) REFERENCES `surgeons` (`Id`),
  CONSTRAINT `FK_StaffWorkingPeriod_Theatres_TheatreId` FOREIGN KEY (`TheatreId`) REFERENCES `theatres` (`Id`)
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
-- Table structure for table `surgerytypetheatretype`
--

DROP TABLE IF EXISTS `surgerytypetheatretype`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `surgerytypetheatretype` (
  `SuitableTheatreTypesId` int NOT NULL,
  `SurgeryTypesConductedId` int NOT NULL,
  PRIMARY KEY (`SuitableTheatreTypesId`,`SurgeryTypesConductedId`),
  KEY `IX_SurgeryTypeTheatreType_SurgeryTypesConductedId` (`SurgeryTypesConductedId`),
  CONSTRAINT `FK_SurgeryTypeTheatreType_SurgeryTypes_SurgeryTypesConductedId` FOREIGN KEY (`SurgeryTypesConductedId`) REFERENCES `surgerytypes` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_SurgeryTypeTheatreType_TheatreTypes_SuitableTheatreTypesId` FOREIGN KEY (`SuitableTheatreTypesId`) REFERENCES `theatretypes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `surgerytypetheatretype`
--

LOCK TABLES `surgerytypetheatretype` WRITE;
/*!40000 ALTER TABLE `surgerytypetheatretype` DISABLE KEYS */;
/*!40000 ALTER TABLE `surgerytypetheatretype` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theatres`
--

LOCK TABLES `theatres` WRITE;
/*!40000 ALTER TABLE `theatres` DISABLE KEYS */;
INSERT INTO `theatres` VALUES (1,1,'Orthapaedic OR 1');
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
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `theatretypes`
--

LOCK TABLES `theatretypes` WRITE;
/*!40000 ALTER TABLE `theatretypes` DISABLE KEYS */;
INSERT INTO `theatretypes` VALUES (1,'Orthapaedic OT'),(2,'General Surgery OT'),(3,'Endoscopy OT');
/*!40000 ALTER TABLE `theatretypes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `workingblocks`
--

DROP TABLE IF EXISTS `workingblocks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `workingblocks` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RemainingTime` time(6) NOT NULL,
  `Start` datetime(6) NOT NULL,
  `End` datetime(6) NOT NULL,
  `Duration` time(6) NOT NULL,
  `TheatreId` int DEFAULT NULL,
  `SurgeonId` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_WorkingBlocks_TheatreId` (`TheatreId`),
  KEY `IX_WorkingBlocks_SurgeonId` (`SurgeonId`),
  CONSTRAINT `FK_WorkingBlocks_Surgeons_SurgeonId` FOREIGN KEY (`SurgeonId`) REFERENCES `surgeons` (`Id`),
  CONSTRAINT `FK_WorkingBlocks_Theatres_TheatreId` FOREIGN KEY (`TheatreId`) REFERENCES `theatres` (`Id`)
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

-- Dump completed on 2022-02-02 12:46:46
