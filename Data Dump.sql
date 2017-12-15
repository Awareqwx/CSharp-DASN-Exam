-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: dojoactivities
-- ------------------------------------------------------
-- Server version	5.7.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `activities`
--

DROP TABLE IF EXISTS `activities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `activities` (
  `ActivityId` int(11) NOT NULL AUTO_INCREMENT,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `Title` varchar(45) DEFAULT NULL,
  `TimeAndDate` datetime DEFAULT NULL,
  `Duration` int(11) DEFAULT NULL,
  `DurationType` varchar(45) DEFAULT NULL,
  `Description` text,
  `CreatorId` int(11) NOT NULL,
  PRIMARY KEY (`ActivityId`),
  UNIQUE KEY `ActivityId_UNIQUE` (`ActivityId`),
  KEY `fk_events_users_idx` (`CreatorId`),
  CONSTRAINT `fk_events_users` FOREIGN KEY (`CreatorId`) REFERENCES `users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `activities`
--

LOCK TABLES `activities` WRITE;
/*!40000 ALTER TABLE `activities` DISABLE KEYS */;
INSERT INTO `activities` VALUES (1,'2017-12-15 12:08:54','2017-12-15 12:08:54','Generic Party','2017-12-30 12:00:00',1,'0','An extremely generic party. There is nothing special about it in the slightest.',1),(2,'2017-12-15 12:36:17','2017-12-15 12:36:17','another party','2017-12-20 03:15:00',1,'0','asdfasdfasdf',1),(4,'2017-12-15 13:23:43','2017-12-15 13:23:43','YEAAAHH!!!','2017-12-18 05:00:00',5,'0','WOOOOOOO!!!!!',2),(5,'2017-12-15 13:24:33','2017-12-15 13:24:33','Nerf War','2017-12-23 13:00:00',3,'0','Bring yo\' guns, we\'re gonna have some fun!',3);
/*!40000 ALTER TABLE `activities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `signups`
--

DROP TABLE IF EXISTS `signups`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `signups` (
  `SignupId` int(11) NOT NULL AUTO_INCREMENT,
  `ActivityId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`SignupId`),
  KEY `fk_events_has_users_users1_idx` (`UserId`),
  KEY `fk_events_has_users_events1_idx` (`ActivityId`),
  CONSTRAINT `fk_events_has_users_events1` FOREIGN KEY (`ActivityId`) REFERENCES `activities` (`ActivityId`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_events_has_users_users1` FOREIGN KEY (`UserId`) REFERENCES `users` (`UserId`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `signups`
--

LOCK TABLES `signups` WRITE;
/*!40000 ALTER TABLE `signups` DISABLE KEYS */;
INSERT INTO `signups` VALUES (1,1,2),(3,2,3),(6,2,2),(7,4,3),(8,1,3),(9,5,2),(10,5,1);
/*!40000 ALTER TABLE `signups` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `created_at` datetime DEFAULT NULL,
  `updated_at` datetime DEFAULT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `Email` varchar(45) DEFAULT NULL,
  `PasswordHash` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'2017-12-15 10:32:55','2017-12-15 10:32:55','John','Smith','johnsmith@foo.bar','AQAAAAEAACcQAAAAEI6okHYadOah95W2Xp7vK8mrREwhzk+5Ltmce5t9WZpTe2LTYfRdOMK2fTBpw94D/g=='),(2,'2017-12-15 10:50:09','2017-12-15 10:50:09','Jane','Smith','janesmith@foo.bar','AQAAAAEAACcQAAAAENR+5SkBUhKGcP7YyEMpzBlHoHuzR/Q99xVv+Slu7ARPDMWj5MIMkRQJ0K28w1TzMQ=='),(3,'2017-12-15 13:17:21','2017-12-15 13:17:21','Will','Fletcher','willfletcher@alpha.tck','AQAAAAEAACcQAAAAEAG9Tejb+4T8N7ucA8xSiFPkXyRZtIipTze7UIW1d9OeJ69jLvSR/NUDk0DyvIuhZw==');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-12-15 13:25:36
