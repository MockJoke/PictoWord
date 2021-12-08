-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Dec 25, 2020 at 09:27 AM
-- Server version: 5.6.12-log
-- PHP Version: 5.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `pictoword`
-- 	

-- --------------------------------------------------------

--
-- Table structure for table `puzzles`
--

CREATE TABLE IF NOT EXISTS `puzzles` (
  `LevelNo` int(50) NOT NULL AUTO_INCREMENT,
  `word` varchar(50) NOT NULL,
  `input` text NOT NULL,
  `hint` text NOT NULL,
  `picture` varchar(100) NOT NULL,
  PRIMARY KEY (`LevelNo`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=25 ;

--
-- Dumping data for table `puzzles`
--

INSERT INTO `puzzles` (`LevelNo`, `word`, `input`, `hint`, `picture`) VALUES
(1, 'bale', 'bqwrtayuilopsdev', '_a_e', 'bale.jpg'),
(2, 'degea', 'dyueiogpsefhajkl', '_e_e_', 'degea.jpg'),
(3, 'dybala', 'dqweyrtubaiooplsa', 'dybala', 'dybala.jpg'),
(4, 'giroud', 'gqwiertyojkluzxd', '_i__u_', 'giroud.jpg'),
(5, 'grealish', 'qwgtyreukaclvish', 'g_e___s_', 'grealish.jpg'),
(6, 'hendo', 'whqetnydpejrlson', '_e__e___n', 'henderson.jpg'),
(7, 'kaka', 'qkwauiertklpacvb', '__k_', 'kaka.jpg'),
(8, 'lampard', 'qwlghambvpnkasrd', '__m__r_', 'lampard.jpg'),
(9, 'mahrez', 'wqmtayuhjkrpeioz', '__h_e_', 'mahrez.jpg'),
(10, 'mata', 'ewmqyaioptklhabn', '__t_', 'mata.jpg'),
(11, 'mbappe', 'rtmqwhbavcpzxpse', '_b__p_', 'mbappe.jpg'),
(12, 'messi', 'pmoyeksqwscvizxn', '_e_s_', 'messi.jpg'),
(13, 'pogba', 'pwqohfgsxbzamnck', '__g_a', 'pogba.jpg'),
(14, 'rashford', 'rplaskjhfgoqwrcd', 'r___f__d', 'rashford.jpg'),
(15, 'rooney', 'pgrohqwobvnexyzf', '_o_n__', 'rooney.jpg'),
(16, 'rose', 'ytrqwosghcepzxnm', '__s_', 'rose.jpg'),
(17, 'salah', 'iusqwalrkjvadhmn', '__l__', 'salah.jpg'),
(18, 'sancho', 'kjsqwapungchbozv', 's_n___', 'sancho.jpg'),
(19, 'silva', 'posqwieckldvxabz', '_i_v_', 'silva.jpg'),
(20, 'son', 'qwesrtouinplkjhm', '_o_', 'son.jpg'),
(21, 'sterling', 'posqwterdlbimncg', 's__r_i__', 'sterling.jpg'),
(22, 'tevez', 'yrtqwebcvexdzfak', '__v_z', 'tevez.jpg'),
(23, 'willian', 'rwtiqplblimazxns', 'w__l_a_', 'willian.jpeg'),
(24, 'zlatan', 'mbzqwlaertkjands', 'z__t_n', 'zlatan.jpg');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
