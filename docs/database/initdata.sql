-- MySQL dump 10.13  Distrib 8.0.11, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ku.core.cms
-- ------------------------------------------------------
-- Server version	8.0.11

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `content_column`
--

DROP TABLE IF EXISTS `content_column`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `content_column` (
  `Id` bigint(20) NOT NULL,
  `CreateTime` datetime(6) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `ParentId` bigint(20) DEFAULT NULL,
  `Name` varchar(100) NOT NULL,
  `Title` varchar(128) DEFAULT NULL,
  `OrderIndex` int(11) NOT NULL,
  `Tag` varchar(64) DEFAULT NULL,
  `Tags` varchar(128) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `content_column`
--

LOCK TABLES `content_column` WRITE;
/*!40000 ALTER TABLE `content_column` DISABLE KEYS */;
INSERT INTO `content_column` VALUES (208156779140349952,'2019-01-10 01:31:31.095750','\0',NULL,'测试新闻栏目',NULL,0,'test.news','||'),(208156926163288064,'2019-01-10 01:32:06.148569','\0',NULL,'测试视频栏目',NULL,0,'test.video','|video|'),(208157767607779328,'2019-01-10 01:35:26.764021','\0',208156779140349952,'栏目1',NULL,0,'test.news.1','||'),(208157809802477568,'2019-01-10 01:35:36.824933','\0',208156779140349952,'栏目2',NULL,0,'test.news.2','||'),(208158664450637824,'2019-01-10 01:39:00.588526','\0',208156926163288064,'视频栏目1',NULL,0,'test.video.1','|video|'),(208158828561170432,'2019-01-10 01:39:39.715896','\0',208156926163288064,'视频栏目2',NULL,0,'test.video.2','|video|');
/*!40000 ALTER TABLE `content_column` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `system_function`
--

DROP TABLE IF EXISTS `system_function`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `system_function` (
  `Id` bigint(20) NOT NULL,
  `AuthCode` varchar(64) NOT NULL,
  `CreateTime` datetime(6) NOT NULL,
  `HasSub` bit(1) NOT NULL,
  `IsEnable` bit(1) NOT NULL,
  `Level` int(11) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `ParentId` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_function`
--

LOCK TABLES `system_function` WRITE;
/*!40000 ALTER TABLE `system_function` DISABLE KEYS */;
INSERT INTO `system_function` VALUES (1,'ku.develop','2017-08-22 00:00:00.000000','','',1,'全功能',NULL),(154378832483188736,'aaa','2018-08-14 15:57:09.466464','\0','',2,'测试功能',1),(154378900816789504,'aaagg','2018-08-14 15:57:25.758867','\0','',2,'大是大非',1);
/*!40000 ALTER TABLE `system_function` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `system_menu`
--

DROP TABLE IF EXISTS `system_menu`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `system_menu` (
  `Id` bigint(20) NOT NULL,
  `AuthCode` varchar(64) NOT NULL,
  `CreateTime` datetime(6) NOT NULL,
  `HasSubMenu` bit(1) NOT NULL,
  `Icon` varchar(20) DEFAULT NULL,
  `IsShow` bit(1) NOT NULL,
  `Name` varchar(20) NOT NULL,
  `OrderIndex` int(11) NOT NULL,
  `ParentId` bigint(20) DEFAULT NULL,
  `Url` varchar(256) DEFAULT NULL,
  `Tag` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_system_menu_ParentId` (`ParentId`),
  CONSTRAINT `FK_system_menu_system_menu_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `system_menu` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_menu`
--

LOCK TABLES `system_menu` WRITE;
/*!40000 ALTER TABLE `system_menu` DISABLE KEYS */;
INSERT INTO `system_menu` VALUES (1,'system','2017-08-22 00:00:00.000000','','fa-cogs','','系统设置',99,NULL,'javascript:;',NULL),(2,'system.menu','2017-08-22 00:00:00.000000','\0',NULL,'','菜单管理',1,1,'/Sys/Menu/Index',NULL),(3,'system.function','2017-08-22 00:00:00.000000','\0',NULL,'','功能设置',2,1,'/Sys/Function/Index',NULL),(25336300164874240,'system.log','2017-08-23 13:48:12.267113','\0',NULL,'','操作日志',99,1,'/System/Log/Index',NULL),(28276112047996928,'materialcenter','2017-08-31 16:29:58.039069','','fa-file-image-o','','素材中心',0,NULL,'javascript:;',NULL),(28276359881031680,'materialcenter.picture','2017-08-31 16:30:57.127847','\0',NULL,'','图片素材',1,28276112047996928,'/MaterialCenter/Picture/Index',NULL),(43460812995559424,'content','2017-10-12 14:08:32.863789','','fa-th-large','','内容管理',0,NULL,'javascript:;',NULL),(81094110840094720,'wechat','2018-01-24 10:29:50.262977','','fa-weixin','','微信管理',0,NULL,'javascript:;',NULL),(81094325571682304,'wechat.account.view','2018-01-24 10:30:41.458285','\0',NULL,'','公众号管理',1,81094110840094720,'/WeChat/WxAccount/Index',NULL),(82913937846173696,'wechat.menu.view','2018-01-29 11:01:10.831786','\0',NULL,'','菜单管理',2,81094110840094720,'/WeChat/WxMenu/Index',NULL),(82967242651729920,'wechat.user.tag.view','2018-01-29 14:32:59.687348','\0',NULL,'','用户标签',10,81094110840094720,'/WeChat/WxUserTag/Index',NULL),(83970999124492288,'wechat.user.view','2018-02-01 09:01:33.879582','\0',NULL,'','用户列表',1,81094110840094720,'/WeChat/WxUser/Index',NULL),(84743890740969472,'wechat.qrcode.view','2018-02-03 12:12:45.600718','\0',NULL,'','微信二维码',9,81094110840094720,'/WeChat/WxQrcode/Index',NULL),(85435072232030208,'mall','2018-02-05 09:59:16.109615','','fa-shopping-cart','','商城管理',0,NULL,'javascript:;',NULL),(110589602540027904,'datacenter','2018-04-15 19:54:23.483295','','fa-server','','数据中心',5,NULL,'javascript:;',NULL),(110589785260687360,'datacenter.app','2018-04-15 19:55:07.047822','',NULL,'','应用管理',0,110589602540027904,'javascript:;',NULL),(110590051330555904,'datacenter.app.view','2018-04-15 19:56:10.483602','\0',NULL,'','应用列表',1,110589785260687360,'/DataCenter/App/Index',NULL),(110826769388404736,'datacenter.app.version','2018-04-16 11:36:48.466857','\0',NULL,'','应用版本',2,110589785260687360,'/DataCenter/AppVersion/Index',NULL),(111532523233738752,'system.notice','2018-04-18 10:21:13.295107','\0',NULL,'','系统公告',6,1,'/Sys/Notice/Index','notice'),(113410300685844480,'content.column.view','2018-04-23 14:42:50.327629','\0',NULL,'','栏目管理',2,43460812995559424,'/Content/Column/Index',NULL),(113692778872963072,'datacenter.app.feedback','2018-04-24 09:25:18.375729','\0',NULL,'','应用反馈',3,110589785260687360,'/DataCenter/AppFeedback/Index',NULL),(114467308893634560,'mall.product','2018-04-26 12:43:00.722581','',NULL,'','商品中心',1,85435072232030208,'javascript:;',NULL),(114468392395603968,'mall.product.category.view','2018-04-26 12:47:19.049616','\0',NULL,'','商品类目',5,114467308893634560,'/Mall/Product/Category/Index',NULL),(114469202760302592,'mall.product.view','2018-04-26 12:50:32.255380','\0',NULL,'','商品列表',1,114467308893634560,'/Mall/Product/Index',NULL),(115698328880021504,'mall.config','2018-04-29 22:14:38.758906','',NULL,'','配置',99,85435072232030208,'javascript:;',NULL),(115698674507448320,'mall.payment.view','2018-04-29 22:16:01.162929','\0',NULL,'','支付方式',1,115698328880021504,'/Mall/Payment/Index',NULL),(115698972319809536,'mall.deliverytemplet.view','2018-04-29 22:17:12.166432','\0',NULL,'','运费模板',2,115698328880021504,'/Mall/DeliveryTemplet/Index',NULL),(121755902960205824,'usercenter','2018-05-16 15:25:16.963692','','fa-user-o','','用户中心',1,NULL,'javascript:;',NULL),(121756445224992768,'usercenter.user.view','2018-05-16 15:27:26.249695','\0',NULL,'','用户列表',1,121755902960205824,'/UserCenter/User/Index',NULL),(121756618713989120,'usercenter.role.view','2018-05-16 15:28:07.612648','\0',NULL,'','角色列表',2,121755902960205824,'/UserCenter/Role/Index',NULL),(123955913496199168,'communication','2018-05-22 17:07:20.349015','','fa-paper-plane','','通信管理',6,NULL,'javascript:;',NULL),(123956175183020032,'communication.sms','2018-05-22 17:08:22.740102','',NULL,'','短信',1,123955913496199168,'javascript:;',NULL),(123956509477437440,'communication.sms.queue','2018-05-22 17:09:42.442660','\0',NULL,'','短信队列',1,123956175183020032,'/Communication/Sms/QueueList',NULL),(123956692101627904,'communication.sms.templet','2018-05-22 17:10:25.983224','\0',NULL,'','短信模板',2,123956175183020032,'/Communication/Sms/TempletList',NULL),(123956849086038016,'communication.sms.account','2018-05-22 17:11:03.411652','\0',NULL,'','短信账户',3,123956175183020032,'/Communication/Sms/AccountList',NULL),(127525165989888000,'usercenter.user.address','2018-06-01 13:30:16.457096','\0',NULL,'','收货地址',3,121755902960205824,'/UserCenter/UserAddress/Index',NULL),(135520173481263104,'usercenter.user.point','2018-06-23 14:59:34.783298','\0',NULL,'','用户积分',4,121755902960205824,'/UserCenter/UserPoint/Index',NULL),(136587499064524800,'mall.brand.view','2018-06-26 13:40:45.032085','\0',NULL,'','品牌管理',2,85435072232030208,'/Mall/Brand/Index',NULL),(137365075034898432,'mall.product.unit.view','2018-06-28 17:10:33.590459','\0',NULL,'','计量单位',3,115698328880021504,'/Mall/Product/Unit/Index',NULL),(138787431460634624,'mall.order','2018-07-02 15:22:29.788872','',NULL,'','订单中心',1,85435072232030208,'javascript:;',NULL),(138788058194509824,'mall.order.view','2018-07-02 15:24:59.213837','\0',NULL,'','订单列表',1,138787431460634624,'/Mall/Order/Index',NULL),(139146502135087104,'content.maskword.view','2018-07-03 15:09:18.908353','\0',NULL,'','屏蔽词',90,43460812995559424,'/Content/MaskWord/Index',NULL),(146453349103828992,'system.timedtask','2018-07-23 19:04:06.980605','\0',NULL,'','定时任务',3,1,'/Sys/TimedTask/Index',NULL),(147071712674971648,'usercenter.user.dialogue.view','2018-07-25 12:01:16.344631','\0',NULL,'','用户留言',5,121755902960205824,'/UserCenter/UserDialogue/Index',NULL),(155704037130371072,'materialcenter.config.edit','2018-08-18 07:43:02.875941','\0',NULL,'','相关配置',99,28276112047996928,'/MaterialCenter/Config/Edit',NULL),(159464236836192256,'datacenter.pageviewrecord.view','2018-08-28 16:44:44.346549','\0',NULL,'','访问记录',1,110589602540027904,'/DataCenter/PageViewRecord/Index',NULL),(161136325611749376,'website','2018-09-02 07:29:01.376622','','fa-desktop','','网站管理',0,NULL,'javascript:;',NULL),(163740034279669760,'content.advertisement.board.view','2018-09-09 11:55:13.897257','\0',NULL,'','广告位管理',71,161136325611749376,'/Content/Advertisement/AdvertisementBoard/Index',NULL),(164608070444384256,'content','2018-09-11 21:24:29.846353','',NULL,'','网站栏目',0,161136325611749376,'javascript:;',NULL),(164608346530250752,'content.article','2018-09-11 21:25:35.670076','\0',NULL,'','测试新闻栏目',0,164608070444384256,'/Content/Article/Index?tag=test.news',NULL),(204294251658346496,'content.video','2018-12-30 09:43:12.781942','',NULL,'','视频管理',7,161136325611749376,NULL,NULL),(204294546413060096,'content.video','2018-12-30 09:44:23.056971','\0',NULL,'','视频列表',1,204294251658346496,'/Content/Article/Index?tag=test.video',NULL),(204294914651979776,'content.video.album','2018-12-30 09:45:50.851805','\0',NULL,'','专辑列表',2,204294251658346496,'/Content/Album/Index',NULL),(205590246283280384,'materialcenter.video','2019-01-02 23:33:01.978228','\0',NULL,'','视频素材',2,28276112047996928,'/MaterialCenter/Video/Index',NULL);
/*!40000 ALTER TABLE `system_menu` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `system_timed_task`
--

DROP TABLE IF EXISTS `system_timed_task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `system_timed_task` (
  `Id` bigint(20) NOT NULL,
  `CreateTime` datetime(6) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  `Group` varchar(64) NOT NULL,
  `Name` varchar(64) NOT NULL,
  `Status` smallint(6) NOT NULL,
  `Cron` varchar(32) NOT NULL,
  `AssemblyName` varchar(128) NOT NULL,
  `TypeName` varchar(128) NOT NULL,
  `StarRunTime` datetime(6) DEFAULT NULL,
  `EndRunTime` datetime(6) DEFAULT NULL,
  `NextRunTime` datetime(6) DEFAULT NULL,
  `ValidStartTime` datetime(6) NOT NULL,
  `ValidEndTime` datetime(6) NOT NULL,
  `RunTimes` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `system_timed_task`
--

LOCK TABLES `system_timed_task` WRITE;
/*!40000 ALTER TABLE `system_timed_task` DISABLE KEYS */;
INSERT INTO `system_timed_task` VALUES (146471776736509952,'2018-07-23 20:17:20.470395','','默认','测试',1,'0 0/1 * * * ? ','Ku.Core.CMS.Job','Ku.Core.CMS.Job.TestJob','2018-09-05 09:14:00.000226','2018-09-05 17:14:00.091994','2018-09-05 09:15:00.000000','2018-07-23 20:16:30.000000','9999-12-31 23:59:59.000000',3),(146502691953049600,'2018-07-23 22:20:11.232336','\0','通信','短信队列发送',1,'0/10 * * * * ? ','Ku.Core.CMS.Job','Ku.Core.CMS.Job.SmsJob','2019-01-09 16:46:30.007326','2019-01-10 00:46:30.187907','2019-01-09 16:46:40.000000','2018-07-23 22:19:01.000000','9999-12-31 23:59:59.000000',4),(162376697331580928,'2018-09-05 17:37:49.039086','\0','素材','图片压缩缩略图',1,'0/30 * * * * ? ','Ku.Core.CMS.Job','Ku.Core.CMS.Job.PictureMaterialJob','2019-01-09 16:46:30.005252','2019-01-10 00:46:30.207675','2019-01-09 16:47:00.000000','2018-09-05 17:36:25.000000','9999-12-31 23:59:59.000000',0),(162376697331580929,'2018-09-05 17:37:49.039086','\0','素材','视频处理',1,'0/30 * * * * ? ','Ku.Core.CMS.Job','Ku.Core.CMS.Job.VideoMaterialJob','2019-01-09 16:46:30.002766','2019-01-10 00:46:30.101525','2019-01-09 16:47:00.000000','2018-09-05 17:36:25.000000','9999-12-31 23:59:59.000000',0),(164286606721679360,'2018-09-11 00:07:06.922616','\0','数据中心','访问记录处理',1,'0/10 * * * * ? ','Ku.Core.CMS.Job','Ku.Core.CMS.Job.PageViewRecordJob','2019-01-09 16:46:30.000270','2019-01-10 00:46:30.093891','2019-01-09 16:46:40.000000','2018-09-11 00:06:32.000000','9999-12-31 23:59:59.000000',0);
/*!40000 ALTER TABLE `system_timed_task` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-10  1:43:33
