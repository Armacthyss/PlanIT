-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               10.4.32-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win64
-- HeidiSQL Version:             12.16.0.7229
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for planitdb
DROP DATABASE IF EXISTS `planitdb`;
CREATE DATABASE IF NOT EXISTS `planitdb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;
USE `planitdb`;

-- Dumping structure for table planitdb.tbl_bookings
DROP TABLE IF EXISTS `tbl_bookings`;
CREATE TABLE IF NOT EXISTS `tbl_bookings` (
  `BookingID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `EventID` int(11) NOT NULL,
  `TicketCount` int(255) NOT NULL DEFAULT 1,
  `BookingDate` datetime DEFAULT current_timestamp(),
  `Status` varchar(50) DEFAULT 'Confirmed',
  PRIMARY KEY (`BookingID`) USING BTREE,
  KEY `fk_booking_user` (`UserID`),
  KEY `fk_booking_event` (`EventID`),
  CONSTRAINT `fk_booking_event` FOREIGN KEY (`EventID`) REFERENCES `tbl_events` (`EventID`),
  CONSTRAINT `fk_booking_user` FOREIGN KEY (`UserID`) REFERENCES `tbl_users` (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table planitdb.tbl_bookings: ~10 rows (approximately)
REPLACE INTO `tbl_bookings` (`BookingID`, `UserID`, `EventID`, `TicketCount`, `BookingDate`, `Status`) VALUES
	(1, 21, 3, 1, '2026-04-25 04:42:35', 'Confirmed'),
	(2, 21, 2, 1, '2026-04-25 04:52:08', 'Confirmed'),
	(3, 21, 2, 1, '2026-04-25 04:52:39', 'Confirmed'),
	(4, 21, 3, 1, '2026-04-25 07:32:26', 'Confirmed'),
	(5, 21, 3, 1, '2026-04-25 07:43:36', 'Confirmed'),
	(6, 21, 4, 1, '2026-04-25 07:45:33', 'Confirmed'),
	(7, 22, 2, 1, '2026-04-25 08:18:48', 'Confirmed'),
	(8, 21, 2, 1, '2026-04-25 08:51:44', 'Confirmed'),
	(9, 21, 2, 1, '2026-05-20 18:45:55', 'Confirmed'),
	(10, 21, 2, 1, '2026-05-20 19:00:01', 'Confirmed');

-- Dumping structure for table planitdb.tbl_events
DROP TABLE IF EXISTS `tbl_events`;
CREATE TABLE IF NOT EXISTS `tbl_events` (
  `EventID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) NOT NULL,
  `EventDate` datetime NOT NULL,
  `Location` varchar(255) DEFAULT NULL,
  `TotalTickets` int(11) DEFAULT 0,
  `Description` text DEFAULT NULL,
  `CreatedAt` datetime DEFAULT current_timestamp(),
  `Price` decimal(18,2) DEFAULT 0.00,
  PRIMARY KEY (`EventID`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table planitdb.tbl_events: ~4 rows (approximately)
REPLACE INTO `tbl_events` (`EventID`, `Title`, `EventDate`, `Location`, `TotalTickets`, `Description`, `CreatedAt`, `Price`) VALUES
	(2, 'Ado’s “Hibana” World Tour', '0001-01-01 00:00:00', 'SM Mall of Asia Arena', 44, 'Japanese music sensation Ado will perform in Manila for the very first time as part of her much-anticipated “Hibana” World Tour. The concert will take place on May 8, 2025, at the SM Mall of Asia Arena, one of the Philippines’ largest and most iconic venues.', '2026-04-25 04:23:49', 0.00),
	(3, 'Hatsune Miku Magic Mirai', '0001-01-01 00:00:00', 'MOA ARENA', 497, '“Hatsune Miku Magical Mirai” is an event that combines a 3DCG live performance by Hatsune Miku and other virtual singers with a special exhibition where visitors can experience the joy of creation.', '2026-04-25 04:28:23', 10.00),
	(4, 'NEw event', '0001-01-01 00:00:00', 'moa', 19, 'new sample event', '2026-04-25 07:44:43', 300.00),
	(5, 'Ado’s “Hibana” World Tour2', '2026-05-13 00:00:00', 'Manila', 0, 'dasdawd', '2026-05-07 10:57:58', 12.00);

-- Dumping structure for table planitdb.tbl_roles
DROP TABLE IF EXISTS `tbl_roles`;
CREATE TABLE IF NOT EXISTS `tbl_roles` (
  `RoleID` int(2) NOT NULL AUTO_INCREMENT,
  `RoleName` varchar(50) DEFAULT NULL,
  `createAt` datetime DEFAULT NULL,
  `updateAt` datetime DEFAULT NULL,
  PRIMARY KEY (`RoleID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table planitdb.tbl_roles: ~0 rows (approximately)

-- Dumping structure for table planitdb.tbl_users
DROP TABLE IF EXISTS `tbl_users`;
CREATE TABLE IF NOT EXISTS `tbl_users` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(25) NOT NULL,
  `LastName` varchar(25) NOT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` varchar(50) DEFAULT NULL,
  `RoleID` int(11) NOT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdateAt` datetime DEFAULT NULL,
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- Dumping data for table planitdb.tbl_users: ~2 rows (approximately)
REPLACE INTO `tbl_users` (`UserID`, `FirstName`, `LastName`, `Email`, `Password`, `RoleID`, `CreatedAt`, `UpdateAt`) VALUES
	(21, 'Renzo', 'Palmon', 'miguelpalmon@gmail.com', '12345600', 2, '2026-04-25 01:58:08', '2026-04-25 01:58:08'),
	(22, 'Mariano', 'lopez', 'mlopez@gmail.com', '123456', 2, '2026-04-25 08:18:22', '2026-04-25 08:18:22');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
