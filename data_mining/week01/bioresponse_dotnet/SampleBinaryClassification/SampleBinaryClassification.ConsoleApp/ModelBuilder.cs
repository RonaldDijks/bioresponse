//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML;
using Microsoft.ML.Data;
using SampleBinaryClassification.Model.DataModels;
using Microsoft.ML.Trainers.LightGbm;

namespace SampleBinaryClassification.ConsoleApp
{
    public static class ModelBuilder
    {
        private static string TRAIN_DATA_FILEPATH = @"/home/ronald/minor/data_mining/week01/bioresponse_dotnet/data/train.csv";
        private static string MODEL_FILEPATH = @"../../../../SampleBinaryClassification.Model/MLModel.zip";

        // Create MLContext to be shared across the model creation workflow objects 
        // Set a random seed for repeatable/deterministic results across multiple trainings.
        private static MLContext mlContext = new MLContext(seed: 1);

        public static void CreateModel()
        {
            // Load Data
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: TRAIN_DATA_FILEPATH,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Build training pipeline
            IEstimator<ITransformer> trainingPipeline = BuildTrainingPipeline(mlContext);

            // Evaluate quality of Model
            Evaluate(mlContext, trainingDataView, trainingPipeline);

            // Train Model
            ITransformer mlModel = TrainModel(mlContext, trainingDataView, trainingPipeline);

            // Save model
            SaveModel(mlContext, mlModel, MODEL_FILEPATH, trainingDataView.Schema);
        }

        public static IEstimator<ITransformer> BuildTrainingPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations 
            var dataProcessPipeline = mlContext.Transforms.Concatenate("Features", new[] { "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "D11", "D12", "D13", "D14", "D15", "D16", "D17", "D18", "D19", "D20", "D21", "D22", "D23", "D24", "D25", "D26", "D27", "D28", "D29", "D30", "D31", "D32", "D33", "D34", "D35", "D36", "D37", "D38", "D39", "D40", "D41", "D42", "D43", "D44", "D45", "D46", "D47", "D48", "D49", "D50", "D51", "D52", "D53", "D54", "D55", "D56", "D57", "D58", "D59", "D60", "D61", "D62", "D63", "D64", "D65", "D66", "D67", "D68", "D69", "D70", "D71", "D72", "D73", "D74", "D75", "D76", "D77", "D78", "D79", "D80", "D81", "D82", "D83", "D84", "D85", "D86", "D87", "D88", "D89", "D90", "D91", "D92", "D93", "D94", "D95", "D96", "D97", "D98", "D99", "D100", "D101", "D102", "D103", "D104", "D105", "D106", "D107", "D108", "D109", "D110", "D111", "D112", "D113", "D114", "D115", "D116", "D117", "D118", "D119", "D120", "D121", "D122", "D123", "D124", "D125", "D126", "D127", "D128", "D129", "D130", "D131", "D132", "D133", "D134", "D135", "D136", "D137", "D138", "D139", "D140", "D141", "D142", "D143", "D144", "D145", "D146", "D147", "D148", "D149", "D150", "D151", "D152", "D153", "D154", "D155", "D156", "D157", "D158", "D159", "D160", "D161", "D162", "D163", "D164", "D165", "D166", "D167", "D168", "D169", "D170", "D171", "D172", "D173", "D174", "D175", "D176", "D177", "D178", "D179", "D180", "D181", "D182", "D183", "D184", "D185", "D186", "D187", "D188", "D189", "D190", "D191", "D192", "D193", "D194", "D195", "D196", "D197", "D198", "D199", "D200", "D201", "D202", "D203", "D204", "D205", "D206", "D207", "D208", "D209", "D210", "D211", "D212", "D213", "D214", "D215", "D216", "D217", "D218", "D219", "D220", "D221", "D222", "D223", "D224", "D225", "D226", "D227", "D228", "D229", "D230", "D231", "D232", "D233", "D234", "D235", "D236", "D237", "D238", "D239", "D240", "D241", "D242", "D243", "D244", "D245", "D246", "D247", "D248", "D249", "D250", "D251", "D252", "D253", "D254", "D255", "D256", "D257", "D258", "D259", "D260", "D261", "D262", "D263", "D264", "D265", "D266", "D267", "D268", "D269", "D270", "D271", "D272", "D273", "D274", "D275", "D276", "D277", "D278", "D279", "D280", "D281", "D282", "D283", "D284", "D285", "D286", "D287", "D288", "D289", "D290", "D291", "D292", "D293", "D294", "D295", "D296", "D297", "D298", "D299", "D300", "D301", "D302", "D303", "D304", "D305", "D306", "D307", "D308", "D309", "D310", "D311", "D312", "D313", "D314", "D315", "D316", "D317", "D318", "D319", "D320", "D321", "D322", "D323", "D324", "D325", "D326", "D327", "D328", "D329", "D330", "D331", "D332", "D333", "D334", "D335", "D336", "D337", "D338", "D339", "D340", "D341", "D342", "D343", "D344", "D345", "D346", "D347", "D348", "D349", "D350", "D351", "D352", "D353", "D354", "D355", "D356", "D357", "D358", "D359", "D360", "D361", "D362", "D363", "D364", "D365", "D366", "D367", "D368", "D369", "D370", "D371", "D372", "D373", "D374", "D375", "D376", "D377", "D378", "D379", "D380", "D381", "D382", "D383", "D384", "D385", "D386", "D387", "D388", "D389", "D390", "D391", "D392", "D393", "D394", "D395", "D396", "D397", "D398", "D399", "D400", "D401", "D402", "D403", "D404", "D405", "D406", "D407", "D408", "D409", "D410", "D411", "D412", "D413", "D414", "D415", "D416", "D417", "D418", "D419", "D420", "D421", "D422", "D423", "D424", "D425", "D426", "D427", "D428", "D429", "D430", "D431", "D432", "D433", "D434", "D435", "D436", "D437", "D438", "D439", "D440", "D441", "D442", "D443", "D444", "D445", "D446", "D447", "D448", "D449", "D450", "D451", "D452", "D453", "D454", "D455", "D456", "D457", "D458", "D459", "D460", "D461", "D462", "D463", "D464", "D465", "D466", "D467", "D468", "D469", "D470", "D471", "D472", "D473", "D474", "D475", "D476", "D477", "D478", "D479", "D480", "D481", "D482", "D483", "D484", "D485", "D486", "D487", "D488", "D489", "D490", "D491", "D492", "D493", "D494", "D495", "D496", "D497", "D498", "D499", "D500", "D501", "D502", "D503", "D504", "D505", "D506", "D507", "D508", "D509", "D510", "D511", "D512", "D513", "D514", "D515", "D516", "D517", "D518", "D519", "D520", "D521", "D522", "D523", "D524", "D525", "D526", "D527", "D528", "D529", "D530", "D531", "D532", "D533", "D534", "D535", "D536", "D537", "D538", "D539", "D540", "D541", "D542", "D543", "D544", "D545", "D546", "D547", "D548", "D549", "D550", "D551", "D552", "D553", "D554", "D555", "D556", "D557", "D558", "D559", "D560", "D561", "D562", "D563", "D564", "D565", "D566", "D567", "D568", "D569", "D570", "D571", "D572", "D573", "D574", "D575", "D576", "D577", "D578", "D579", "D580", "D581", "D582", "D583", "D584", "D585", "D586", "D587", "D588", "D589", "D590", "D591", "D592", "D593", "D594", "D595", "D596", "D597", "D598", "D599", "D600", "D601", "D602", "D603", "D604", "D605", "D606", "D607", "D608", "D609", "D610", "D611", "D612", "D613", "D614", "D615", "D616", "D617", "D618", "D619", "D620", "D621", "D622", "D623", "D624", "D625", "D626", "D627", "D628", "D629", "D630", "D631", "D632", "D633", "D634", "D635", "D636", "D637", "D638", "D639", "D640", "D641", "D642", "D643", "D644", "D645", "D646", "D647", "D648", "D649", "D650", "D651", "D652", "D653", "D654", "D655", "D656", "D657", "D658", "D659", "D660", "D661", "D662", "D663", "D664", "D665", "D666", "D667", "D668", "D669", "D670", "D671", "D672", "D673", "D674", "D675", "D676", "D677", "D678", "D679", "D680", "D681", "D682", "D683", "D684", "D685", "D686", "D687", "D688", "D689", "D690", "D691", "D692", "D693", "D694", "D695", "D696", "D697", "D698", "D699", "D700", "D701", "D702", "D703", "D704", "D705", "D706", "D707", "D708", "D709", "D710", "D711", "D712", "D713", "D714", "D715", "D716", "D717", "D718", "D719", "D720", "D721", "D722", "D723", "D724", "D725", "D726", "D727", "D728", "D729", "D730", "D731", "D732", "D733", "D734", "D735", "D736", "D737", "D738", "D739", "D740", "D741", "D742", "D743", "D744", "D745", "D746", "D747", "D748", "D749", "D750", "D751", "D752", "D753", "D754", "D755", "D756", "D757", "D758", "D759", "D760", "D761", "D762", "D763", "D764", "D765", "D766", "D767", "D768", "D769", "D770", "D771", "D772", "D773", "D774", "D775", "D776", "D777", "D778", "D779", "D780", "D781", "D782", "D783", "D784", "D785", "D786", "D787", "D788", "D789", "D790", "D791", "D792", "D793", "D794", "D795", "D796", "D797", "D798", "D799", "D800", "D801", "D802", "D803", "D804", "D805", "D806", "D807", "D808", "D809", "D810", "D811", "D812", "D813", "D814", "D815", "D816", "D817", "D818", "D819", "D820", "D821", "D822", "D823", "D824", "D825", "D826", "D827", "D828", "D829", "D830", "D831", "D832", "D833", "D834", "D835", "D836", "D837", "D838", "D839", "D840", "D841", "D842", "D843", "D844", "D845", "D846", "D847", "D848", "D849", "D850", "D851", "D852", "D853", "D854", "D855", "D856", "D857", "D858", "D859", "D860", "D861", "D862", "D863", "D864", "D865", "D866", "D867", "D868", "D869", "D870", "D871", "D872", "D873", "D874", "D875", "D876", "D877", "D878", "D879", "D880", "D881", "D882", "D883", "D884", "D885", "D886", "D887", "D888", "D889", "D890", "D891", "D892", "D893", "D894", "D895", "D896", "D897", "D898", "D899", "D900", "D901", "D902", "D903", "D904", "D905", "D906", "D907", "D908", "D909", "D910", "D911", "D912", "D913", "D914", "D915", "D916", "D917", "D918", "D919", "D920", "D921", "D922", "D923", "D924", "D925", "D926", "D927", "D928", "D929", "D930", "D931", "D932", "D933", "D934", "D935", "D936", "D937", "D938", "D939", "D940", "D941", "D942", "D943", "D944", "D945", "D946", "D947", "D948", "D949", "D950", "D951", "D952", "D953", "D954", "D955", "D956", "D957", "D958", "D959", "D960", "D961", "D962", "D963", "D964", "D965", "D966", "D967", "D968", "D969", "D970", "D971", "D972", "D973", "D974", "D975", "D976", "D977", "D978", "D979", "D980", "D981", "D982", "D983", "D984", "D985", "D986", "D987", "D988", "D989", "D990", "D991", "D992", "D993", "D994", "D995", "D996", "D997", "D998", "D999", "D1000", "D1001", "D1002", "D1003", "D1004", "D1005", "D1006", "D1007", "D1008", "D1009", "D1010", "D1011", "D1012", "D1013", "D1014", "D1015", "D1016", "D1017", "D1018", "D1019", "D1020", "D1021", "D1022", "D1023", "D1024", "D1025", "D1026", "D1027", "D1028", "D1029", "D1030", "D1031", "D1032", "D1033", "D1034", "D1035", "D1036", "D1037", "D1038", "D1039", "D1040", "D1041", "D1042", "D1043", "D1044", "D1045", "D1046", "D1047", "D1048", "D1049", "D1050", "D1051", "D1052", "D1053", "D1054", "D1055", "D1056", "D1057", "D1058", "D1059", "D1060", "D1061", "D1062", "D1063", "D1064", "D1065", "D1066", "D1067", "D1068", "D1069", "D1070", "D1071", "D1072", "D1073", "D1074", "D1075", "D1076", "D1077", "D1078", "D1079", "D1080", "D1081", "D1082", "D1083", "D1084", "D1085", "D1086", "D1087", "D1088", "D1089", "D1090", "D1091", "D1092", "D1093", "D1094", "D1095", "D1096", "D1097", "D1098", "D1099", "D1100", "D1101", "D1102", "D1103", "D1104", "D1105", "D1106", "D1107", "D1108", "D1109", "D1110", "D1111", "D1112", "D1113", "D1114", "D1115", "D1116", "D1117", "D1118", "D1119", "D1120", "D1121", "D1122", "D1123", "D1124", "D1125", "D1126", "D1127", "D1128", "D1129", "D1130", "D1131", "D1132", "D1133", "D1134", "D1135", "D1136", "D1137", "D1138", "D1139", "D1140", "D1141", "D1142", "D1143", "D1144", "D1145", "D1146", "D1147", "D1148", "D1149", "D1150", "D1151", "D1152", "D1153", "D1154", "D1155", "D1156", "D1157", "D1158", "D1159", "D1160", "D1161", "D1162", "D1163", "D1164", "D1165", "D1166", "D1167", "D1168", "D1169", "D1170", "D1171", "D1172", "D1173", "D1174", "D1175", "D1176", "D1177", "D1178", "D1179", "D1180", "D1181", "D1182", "D1183", "D1184", "D1185", "D1186", "D1187", "D1188", "D1189", "D1190", "D1191", "D1192", "D1193", "D1194", "D1195", "D1196", "D1197", "D1198", "D1199", "D1200", "D1201", "D1202", "D1203", "D1204", "D1205", "D1206", "D1207", "D1208", "D1209", "D1210", "D1211", "D1212", "D1213", "D1214", "D1215", "D1216", "D1217", "D1218", "D1219", "D1220", "D1221", "D1222", "D1223", "D1224", "D1225", "D1226", "D1227", "D1228", "D1229", "D1230", "D1231", "D1232", "D1233", "D1234", "D1235", "D1236", "D1237", "D1238", "D1239", "D1240", "D1241", "D1242", "D1243", "D1244", "D1245", "D1246", "D1247", "D1248", "D1249", "D1250", "D1251", "D1252", "D1253", "D1254", "D1255", "D1256", "D1257", "D1258", "D1259", "D1260", "D1261", "D1262", "D1263", "D1264", "D1265", "D1266", "D1267", "D1268", "D1269", "D1270", "D1271", "D1272", "D1273", "D1274", "D1275", "D1276", "D1277", "D1278", "D1279", "D1280", "D1281", "D1282", "D1283", "D1284", "D1285", "D1286", "D1287", "D1288", "D1289", "D1290", "D1291", "D1292", "D1293", "D1294", "D1295", "D1296", "D1297", "D1298", "D1299", "D1300", "D1301", "D1302", "D1303", "D1304", "D1305", "D1306", "D1307", "D1308", "D1309", "D1310", "D1311", "D1312", "D1313", "D1314", "D1315", "D1316", "D1317", "D1318", "D1319", "D1320", "D1321", "D1322", "D1323", "D1324", "D1325", "D1326", "D1327", "D1328", "D1329", "D1330", "D1331", "D1332", "D1333", "D1334", "D1335", "D1336", "D1337", "D1338", "D1339", "D1340", "D1341", "D1342", "D1343", "D1344", "D1345", "D1346", "D1347", "D1348", "D1349", "D1350", "D1351", "D1352", "D1353", "D1354", "D1355", "D1356", "D1357", "D1358", "D1359", "D1360", "D1361", "D1362", "D1363", "D1364", "D1365", "D1366", "D1367", "D1368", "D1369", "D1370", "D1371", "D1372", "D1373", "D1374", "D1375", "D1376", "D1377", "D1378", "D1379", "D1380", "D1381", "D1382", "D1383", "D1384", "D1385", "D1386", "D1387", "D1388", "D1389", "D1390", "D1391", "D1392", "D1393", "D1394", "D1395", "D1396", "D1397", "D1398", "D1399", "D1400", "D1401", "D1402", "D1403", "D1404", "D1405", "D1406", "D1407", "D1408", "D1409", "D1410", "D1411", "D1412", "D1413", "D1414", "D1415", "D1416", "D1417", "D1418", "D1419", "D1420", "D1421", "D1422", "D1423", "D1424", "D1425", "D1426", "D1427", "D1428", "D1429", "D1430", "D1431", "D1432", "D1433", "D1434", "D1435", "D1436", "D1437", "D1438", "D1439", "D1440", "D1441", "D1442", "D1443", "D1444", "D1445", "D1446", "D1447", "D1448", "D1449", "D1450", "D1451", "D1452", "D1453", "D1454", "D1455", "D1456", "D1457", "D1458", "D1459", "D1460", "D1461", "D1462", "D1463", "D1464", "D1465", "D1466", "D1467", "D1468", "D1469", "D1470", "D1471", "D1472", "D1473", "D1474", "D1475", "D1476", "D1477", "D1478", "D1479", "D1480", "D1481", "D1482", "D1483", "D1484", "D1485", "D1486", "D1487", "D1488", "D1489", "D1490", "D1491", "D1492", "D1493", "D1494", "D1495", "D1496", "D1497", "D1498", "D1499", "D1500", "D1501", "D1502", "D1503", "D1504", "D1505", "D1506", "D1507", "D1508", "D1509", "D1510", "D1511", "D1512", "D1513", "D1514", "D1515", "D1516", "D1517", "D1518", "D1519", "D1520", "D1521", "D1522", "D1523", "D1524", "D1525", "D1526", "D1527", "D1528", "D1529", "D1530", "D1531", "D1532", "D1533", "D1534", "D1535", "D1536", "D1537", "D1538", "D1539", "D1540", "D1541", "D1542", "D1543", "D1544", "D1545", "D1546", "D1547", "D1548", "D1549", "D1550", "D1551", "D1552", "D1553", "D1554", "D1555", "D1556", "D1557", "D1558", "D1559", "D1560", "D1561", "D1562", "D1563", "D1564", "D1565", "D1566", "D1567", "D1568", "D1569", "D1570", "D1571", "D1572", "D1573", "D1574", "D1575", "D1576", "D1577", "D1578", "D1579", "D1580", "D1581", "D1582", "D1583", "D1584", "D1585", "D1586", "D1587", "D1588", "D1589", "D1590", "D1591", "D1592", "D1593", "D1594", "D1595", "D1596", "D1597", "D1598", "D1599", "D1600", "D1601", "D1602", "D1603", "D1604", "D1605", "D1606", "D1607", "D1608", "D1609", "D1610", "D1611", "D1612", "D1613", "D1614", "D1615", "D1616", "D1617", "D1618", "D1619", "D1620", "D1621", "D1622", "D1623", "D1624", "D1625", "D1626", "D1627", "D1628", "D1629", "D1630", "D1631", "D1632", "D1633", "D1634", "D1635", "D1636", "D1637", "D1638", "D1639", "D1640", "D1641", "D1642", "D1643", "D1644", "D1645", "D1646", "D1647", "D1648", "D1649", "D1650", "D1651", "D1652", "D1653", "D1654", "D1655", "D1656", "D1657", "D1658", "D1659", "D1660", "D1661", "D1662", "D1663", "D1664", "D1665", "D1666", "D1667", "D1668", "D1669", "D1670", "D1671", "D1672", "D1673", "D1674", "D1675", "D1676", "D1677", "D1678", "D1679", "D1680", "D1681", "D1682", "D1683", "D1684", "D1685", "D1686", "D1687", "D1688", "D1689", "D1690", "D1691", "D1692", "D1693", "D1694", "D1695", "D1696", "D1697", "D1698", "D1699", "D1700", "D1701", "D1702", "D1703", "D1704", "D1705", "D1706", "D1707", "D1708", "D1709", "D1710", "D1711", "D1712", "D1713", "D1714", "D1715", "D1716", "D1717", "D1718", "D1719", "D1720", "D1721", "D1722", "D1723", "D1724", "D1725", "D1726", "D1727", "D1728", "D1729", "D1730", "D1731", "D1732", "D1733", "D1734", "D1735", "D1736", "D1737", "D1738", "D1739", "D1740", "D1741", "D1742", "D1743", "D1744", "D1745", "D1746", "D1747", "D1748", "D1749", "D1750", "D1751", "D1752", "D1753", "D1754", "D1755", "D1756", "D1757", "D1758", "D1759", "D1760", "D1761", "D1762", "D1763", "D1764", "D1765", "D1766", "D1767", "D1768", "D1769", "D1770", "D1771", "D1772", "D1773", "D1774", "D1775", "D1776" });

            // Set the training algorithm 
            var trainer = mlContext.BinaryClassification.Trainers.LightGbm(new LightGbmBinaryTrainer.Options() { NumberOfIterations = 200, LearningRate = 0.09528744f, NumberOfLeaves = 25, MinimumExampleCountPerLeaf = 1, UseCategoricalSplit = true, HandleMissingValue = false, MinimumExampleCountPerGroup = 10, MaximumCategoricalSplitPointCount = 32, CategoricalSmoothing = 10, L2CategoricalRegularization = 10, Booster = new GradientBooster.Options() { L2Regularization = 0.5, L1Regularization = 0 }, LabelColumnName = "Activity", FeatureColumnName = "Features" });
            var trainingPipeline = dataProcessPipeline.Append(trainer);

            return trainingPipeline;
        }

        public static ITransformer TrainModel(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            Console.WriteLine("=============== Training  model ===============");

            ITransformer model = trainingPipeline.Fit(trainingDataView);

            Console.WriteLine("=============== End of training process ===============");
            return model;
        }

        private static void Evaluate(MLContext mlContext, IDataView trainingDataView, IEstimator<ITransformer> trainingPipeline)
        {
            // Cross-Validate with single dataset (since we don't have two datasets, one for training and for evaluate)
            // in order to evaluate and get the model's accuracy metrics
            Console.WriteLine("=============== Cross-validating to get model's accuracy metrics ===============");
            var crossValidationResults = mlContext.BinaryClassification.CrossValidateNonCalibrated(trainingDataView, trainingPipeline, numberOfFolds: 5, labelColumnName: "Activity");
            PrintBinaryClassificationFoldsAverageMetrics(crossValidationResults);
        }
        private static void SaveModel(MLContext mlContext, ITransformer mlModel, string modelRelativePath, DataViewSchema modelInputSchema)
        {
            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($"=============== Saving the model  ===============");
            mlContext.Model.Save(mlModel, modelInputSchema, GetAbsolutePath(modelRelativePath));
            Console.WriteLine("The model is saved to {0}", GetAbsolutePath(modelRelativePath));
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }

        public static void PrintBinaryClassificationMetrics(BinaryClassificationMetrics metrics)
        {
            Console.WriteLine($"************************************************************");
            Console.WriteLine($"*       Metrics for binary classification model      ");
            Console.WriteLine($"*-----------------------------------------------------------");
            Console.WriteLine($"*       Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"*       Auc:      {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"************************************************************");
        }


        public static void PrintBinaryClassificationFoldsAverageMetrics(IEnumerable<TrainCatalogBase.CrossValidationResult<BinaryClassificationMetrics>> crossValResults)
        {
            var metricsInMultipleFolds = crossValResults.Select(r => r.Metrics);

            var AccuracyValues = metricsInMultipleFolds.Select(m => m.Accuracy);
            var AccuracyAverage = AccuracyValues.Average();
            var AccuraciesStdDeviation = CalculateStandardDeviation(AccuracyValues);
            var AccuraciesConfidenceInterval95 = CalculateConfidenceInterval95(AccuracyValues);


            Console.WriteLine($"*************************************************************************************************************");
            Console.WriteLine($"*       Metrics for Binary Classification model      ");
            Console.WriteLine($"*------------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"*       Average Accuracy:    {AccuracyAverage:0.###}  - Standard deviation: ({AccuraciesStdDeviation:#.###})  - Confidence Interval 95%: ({AccuraciesConfidenceInterval95:#.###})");
            Console.WriteLine($"*************************************************************************************************************");
        }

        public static double CalculateStandardDeviation(IEnumerable<double> values)
        {
            double average = values.Average();
            double sumOfSquaresOfDifferences = values.Select(val => (val - average) * (val - average)).Sum();
            double standardDeviation = Math.Sqrt(sumOfSquaresOfDifferences / (values.Count() - 1));
            return standardDeviation;
        }

        public static double CalculateConfidenceInterval95(IEnumerable<double> values)
        {
            double confidenceInterval95 = 1.96 * CalculateStandardDeviation(values) / Math.Sqrt((values.Count() - 1));
            return confidenceInterval95;
        }
    }
}
