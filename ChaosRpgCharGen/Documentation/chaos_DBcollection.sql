CREATE DATABASE chaos_datas CHARACTER SET "utf8" COLLATE utf8_hungarian_ci;
CREATE TABLE chaos_disciplines (
	discipl_id integer (3) PRIMARY KEY,
	discipl_name varchar(100),
	discipl_type integer(1),
	discipl_studyReqImprt integer(1),
	discipl_studiab integer(1)
);
INSERT INTO chaos_disciplines (discipl_id, discipl_name, discipl_type, 
discipl_studyReqImprt, discipl_studiab) VALUES 
(1,"Akrobatika",1,1,0),
(2,"Alkudozás",1,1,0),
(3,"Állatgondozás",1,1,1),
(4,"Csomózás",1,1,1),
(5,"Emberismeret",1,1,0),
(6,"Esés",1,1,0),
(7,"Figyelem",1,1,0),
(8,"Futás",1,1,0),
(9,"Hajítás",1,1,0),
(10,"Hallgazózás",1,1,0),
(11,"Hasbeszéd",1,1,0),
(12,"Helyismeret",1,1,0),
(13,"Idomítás",1,1,0),
(14,"Lovaglás",1,1,0),
(15,"Mászás",1,1,0),
(16,"Nyomolvasás",1,1,0),
(17,"Testváltás",1,1,0),
(18,"Ugrás",1,1,0),
(19,"Úszás",1,1,0),
(20,"Vadászat",1,1,0),
(21,"Asztalos",2,1,1),
(22,"Bányászat",2,1,1),
(23,"Becsüs",2,1,1),
(24,"Bűvész",2,1,1),
(25,"Fazekas",2,1,0),
(26,"Fogathajtó",2,1,0),
(27,"Földműves",2,1,0),
(28,"Hajós",2,1,0),
(29,"Halász",2,1,0),
(30,"Kereskedő",2,1,0),
(31,"Kovács",2,1,1),
(32,"Szabó",2,1,0),
(33,"Takács",2,1,0),
(34,"Tímár",2,1,0),
(35,"Szakács",2,1,0),
(36,"Vendéglátó",2,1,0),
(37,"Zsonglőr",2,1,0),
(38,"Fegyverhasználat",3,1,0),
(39,"Cselezés",3,1,1),
(40,"Csonkítás",3,1,0),
(41,"Fantomsebzés",3,1,1),
(42,"Fegyvertörés",3,1,0),
(43,"Feldöntés",3,1,0),
(44,"Harcőrület",3,1,0),
(45,"Két fegyver használat",3,1,0),
(46,"Kifárasztás",3,1,0),
(47,"Lefegyverzés",3,1,0),
(48,"Mesterlövészet",3,1,0),
(49,"Pusztakezes harc",3,1,0),
(50,"Pusztító csapás",3,1,0),
(51,"Rögtönzött fegyver",3,1,0),
(52,"Szimultán harc",3,1,0),
(53,"Vakharc",3,1,1),
(54,"Vértviselet",3,1,0),
(55,"Visszatámadás",3,1,0),
(56,"Álcázás",4,1,0),
(57,"Átverés",4,1,0),
(58,"Csapdakészítés",4,1,1),
(59,"Csapda/rejtekajtó kereség",4,1,0),
(60,"Csapdák eltávolítása",4,1,1),
(61,"Hamisítás",4,1,0),
(62,"Hangutánzás",4,1,0),
(63,"Hátbaszúrás",4,1,0),
(64,"Követés/lerázás",4,1,0),
(65,"Lopakodás",4,1,0),
(66,"Méregkeverés",4,1,1),
(67,"Rejtőzés",4,1,0),
(68,"Szájról olvasás",4,1,0),
(69,"Szerencsejáték",4,1,0),
(70,"Tolvajnyelv/jelbeszéd",4,1,1),
(71,"Tolvajlás",4,1,0),
(72,"Vallatás",4,1,0),
(73,"Zárnyitás",4,1,0),
(74,"Elhivatottság",5,1,0),
(75,"Eszenciakontol",5,1,0),
(76,"Mágiahasználat",5,1,1),
(77,"Pszihasználat",5,1,1),
(78,"Aktív fókusz",5,1,1),
(79,"Aszkézis",5,1,0),
(80,"Áldozat/meditáció/felfrissülés",5,1,1),
(81,"Eszencia felszabadítása",5,1,0),
(82,"Eszenciapótlás",5,1,1),
(83,"Pszi támadás",5,1,1),
(84,"Pszichonikus erő növelése",5,1,1),
(85,"Rúnabontás",5,1,1),
(86,"Rúnarajzolás",5,1,1),
(87,"Varázserő növelése",5,1,1),
(88,"Varázsírás",5,1,1),
(89,"Varázsszer készítés",5,1,1),
(90,"Varázstárgy készítés",5,1,1),
(91,"Varázstekercs uralása",5,1,0),
(92,"Vérúr",5,1,0),
(93,"Zónaérzékelés",5,1,1),
(94,"Zóna kiterjesztése",5,1,1),
(95,"Eszenciapajzs aktiválás",6,0,0),
(96,"Eszenciapáncél",6,0,0),
(97,"Érzelmi kontroll",6,1,0),
(98,"Fájdalomtűrés",6,1,0),
(99,"Koncentráció",6,1,0),
(100,"Lerázás",6,0,1),
(101,"Lélegzet visszatartása",6,1,0),
(102,"Lélekpajzs",6,0,1),
(103,"Pszi-védelem",6,0,0),
(104,"Sebcsökkentés",6,1,0),
(105,"Szabadulás",6,1,0),
(106,"Vetődés",6,0,1),
(107,"Védekezés",6,1,0),
(108,"Zsongás",6,0,0),
(109,"Anyanyelv ismeret",7,1,0),
(110,"Démonismeret",7,1,1),
(111,"Etikett",7,1,1),
(112,"Élőholtak ismerete",7,1,1),
(113,"Fajok ismeret",7,1,1),
(114,"Fegyverismeret",7,1,1),
(115,"Gyógynövény ismeret",7,1,1),
(116,"Hitelmélet",7,1,1),
(117,"Írás/olvasás",7,1,1),
(118,"Irodalom/legendaismeret",7,1,1),
(119,"Mágiaelmélet",7,1,1),
(120,"Méregismeret",7,1,1),
(121,"Nemesség/heraldika",7,1,1),
(122,"Nyelvtudás",7,1,1),
(123,"Őselem ismeret",7,1,1),
(124,"Pszihonika",7,1,1),
(125,"Rúnaismeret",7,1,1),
(126,"Szörnyismeret",7,1,1),
(127,"Természetismeret",7,1,0),
(128,"Varázstárgy ismeret",7,1,0),
(129,"Alkímia",8,1,1),
(130,"Anatómia/élerran",8,1,1),
(131,"Csillagászat",8,1,1),
(132,"Építészet",8,1,1),
(133,"Geográfia",8,1,1),
(134,"Gyógyszerkészítés",8,1,1),
(135,"Holtnyelv ismeret",8,1,1),
(136,"Létsík ismeret",8,1,1),
(137,"Mechanika",8,1,1),
(138,"Mértan",8,1,1),
(139,"Oktatás",8,1,1),
(140,"Orvoslás",8,1,1),
(141,"Politika/törvényismeret",8,1,1),
(142,"Stratégia",8,1,1),
(143,"Számtan",8,1,1),
(144,"Taktika",8,1,0),
(145,"Titkosírás",8,1,0),
(146,"Történelem",8,1,1),
(147,"Uralkodás/vezetés",8,1,1),
(148,"Ékszerművészet",9,1,0),
(149,"Ének",9,1,0),
(150,"Festés",9,1,0),
(151,"Irodalom",9,1,0),
(152,"Kalligráfia",9,1,0),
(153,"Kertészet",9,1,0),
(154,"Mirákulum",9,1,0),
(155,"Rajzolás",9,1,0),
(156,"Szexualitás",9,1,0),
(157,"Színművészet",9,1,0),
(158,"Szobrászat",9,1,1),
(159,"Szónoklás",9,1,1),
(160,"Tánc/mozgásművészet",9,1,0),
(161,"Tetoválás",9,1,0),
(162,"Zeneművészet",9,1,0)
;
CREATE TABLE chaos_discipline_requrement(
	discipl_id integer(3),
	discipl_requirNormGroup integer(1),
	discipl_requirId integer(3),
	discipl_requirTypeDef integer(1)
);
INSERT INTO chaos_discipline_requrement (discipl_id, discipl_requirNormGroup, 
discipl_requirId, discipl_requirTypeDef) VALUES 
(1,1,6,0),
(13,1,3,0),
(20,1,127,0),
(23,1,143,0),
(28,1,0,0),
(28,2,4,0),
(36,1,111,0),
(39,1,38,0),
(40,1,38,0),
(41,1,130,0),
(42,1,114,0),
(44,1,38,0),
(44,1,51,0),
(44,1,49,0),
(45,1,38,0),
(46,1,38,0),
(47,1,38,0),
(47,1,49,0),
(48,1,38,0),
(50,1,38,0),
(50,1,130,0),
(52,1,38,0),
(52,2,49,0),
(55,1,38,0),
(58,1,0,0),
(58,2,137,0),
(60,1,0,0),
(60,2,137,0),
(61,1,2,1),
(61,2,9,1),
(63,1,38,0),
(66,1,120,0),
(70,1,109,0),
(72,1,130,0),
(73,1,0,0),
(73,2,137,0),
(74,1,116,0),
(74,2,999,1),
(76,1,119,0),
(77,1,0,0),
(77,2,124,0),
(78,1,77,0),
(79,1,76,0),
(79,2,74,0),
(80,1,74,0),
(80,2,76,0),
(80,3,77,0),
(81,1,76,0),
(81,2,74,0),
(82,1,81,0),
(83,1,77,0),
(84,1,78,0),
(85,1,125,0),
(86,1,125,0),
(86,1,152,0),
(86,2,125,0),
(86,2,31,0),
(86,3,125,0),
(86,3,158,0),
(87,1,81,0),
(88,1,76,0),
(88,1,117,0),
(88,1,135,0),
(88,2,74,0),
(88,2,117,0),
(88,2,135,0),
(89,1,76,0),
(89,1,129,0),
(89,2,74,0),
(89,2,129,0),
(89,3,119,0),
(89,3,129,0),
(90,1,128,0),
(90,1,76,0),
(90,2,128,0),
(90,2,74,0),
(92,1,76,0),
(92,2,74,0),
(92,3,75,0),
(93,1,76,0),
(93,2,74,0),
(93,3,75,0),
(94,1,93,0),
(94,2,77,0),
(95,1,119,0),
(95,2,76,0),
(95,3,74,0),
(95,4,75,0),
(96,1,119,0),
(96,2,76,0),
(96,3,74,0),
(96,4,75,0),
(100,1,99,0),
(102,1,99,0),
(103,1,124,0),
(106,1,99,0),
(108,1,99,0),
(117,1,109,0),
(120,1,115,0),
(120,2,127,0),
(129,1,143,0),
(131,1,138,0),
(132,1,138,0),
(135,1,109,0),
(137,1,138,0),
(138,1,143,0),
(140,1,130,0),
(142,1,144,0),
(145,1,117,0),
(147,1,5,0),
(148,1,31,0),
(150,1,155,0),
(151,1,117,0),
(152,1,117,0),
(152,1,155,0),
(153,1,127,0),
(153,2,27,0),
(156,1,5,0),
(159,1,109,0),
(159,1,2,0),
(159,2,122,0),
(159,2,2,0),
(161,1,155,0)
;
CREATE TABLE chaos_discipline_attribute(
	discipl_id integer (3),
	discipl_attribId integer(3),
	discipl_JPschema integer(3)
);
INSERT INTO chaos_discipline_attribute (discipl_id, discipl_attribId, discipl_JPschema) VALUES
(1,4,50),
(2,9,40),
(3,7,30),
(4,5,40),
(5,7,50),
(6,4,40),
(7,7,40),
(8,3,50),
(9,2,40),
(10,7,30),
(11,5,50),
(12,8,30),
(13,4,50),
(14,5,40),
(15,5,50),
(16,7,50),
(17,9,30),
(18,4,40),
(19,3,30),
(20,3,40),
(21,5,40),
(22,1,50),
(23,7,40),
(24,5,50),
(25,5,40),
(26,1,40),
(27,1,30),
(28,1,40),
(29,1,40),
(30,7,40),
(31,2,50),
(32,5,40),
(33,5,30),
(34,3,40),
(35,5,30),
(36,9,30),
(37,4,50),
(38,4,40),
(38,5,40),
(39,4,50),
(40,4,50),
(41,4,60),
(42,2,60),
(43,2,50),
(44,9,40),
(45,4,60),
(46,3,40),
(47,6,60),
(48,4,60),
(49,3,30),
(50,2,60),
(51,4,40),
(52,4,50),
(53,6,70),
(54,3,40),
(55,6,70),
(56,5,40),
(57,7,40),
(58,8,40),
(59,7,40),
(60,4,50),
(61,5,50),
(62,5,50),
(63,4,50),
(64,6,50),
(65,4,50),
(66,8,50),
(67,5,40),
(68,8,40),
(69,7,50),
(70,8,30),
(71,5,50),
(72,7,60),
(73,5,50),
(74,9,40),
(74,7,30),
(75,10,60),
(75,7,40),
(76,10,50),
(76,7,40),
(77,7,50),
(77,9,40),
(78,8,50),
(78,9,40),
(79,9,50),
(79,10,40),
(80,9,50),
(80,10,40),
(81,10,50),
(81,7,40),
(82,10,50),
(82,7,40),
(82,9,40),
(83,8,50),
(83,7,40),
(84,8,70),
(84,7,60),
(85,7,50),
(85,10,40),
(86,5,60),
(86,10,40),
(87,7,70),
(87,10,60),
(88,10,50),
(88,7,60),
(89,11,60),
(89,7,40),
(90,10,60),
(90,7,40),
(91,7,70),
(91,10,40),
(92,7,50),
(92,10,40),
(93,10,60),
(93,7,50),
(94,10,60),
(94,7,50),
(95,10,60),
(96,10,60),
(97,9,60),
(98,9,70),
(99,9,50),
(100,3,40),
(101,3,30),
(102,7,40),
(103,7,50),
(103,8,40),
(104,3,50),
(105,5,40),
(106,6,40),
(107,5,30),
(108,7,40),
(109,8,30),
(110,8,40),
(111,8,40),
(112,8,40),
(113,8,40),
(114,8,30),
(115,8,30),
(116,8,50),
(117,8,40),
(118,8,30),
(119,8,50),
(120,8,40),
(121,8,40),
(122,8,40),
(123,8,40),
(124,8,40),
(125,8,40),
(126,8,40),
(127,8,30),
(128,8,30),
(129,8,50),
(130,8,40),
(131,8,50),
(132,8,50),
(133,8,40),
(134,8,40),
(135,8,60),
(136,8,50),
(137,8,50),
(138,8,50),
(139,7,40),
(140,7,40),
(141,8,40),
(142,8,60),
(143,8,40),
(144,8,50),
(145,7,60),
(146,8,40),
(147,7,70),
(148,5,50),
(149,7,50),
(149,9,50),
(150,5,50),
(151,8,50),
(152,5,50),
(153,4,50),
(154,8,50),
(155,5,50),
(156,9,50),
(157,7,50),
(158,5,50),
(159,7,50),
(160,5,40),
(161,5,60),
(162,5,50)
;
CREATE TABLE chaos_discipline_specialis(
	discipl_id integer(3),
	discipl_specialReqGroup integer(1),
	discipl_specialReqId varchar(3),
	discipl_specialLevel integer(3),
	discipl_specialThisMustRise integer(1),
	spec_requestIType integer(1)
);
INSERT INTO chaos_discipline_specialis (discipl_id, discipl_specialReqGroup, 
discipl_specialReqId, discipl_specialLevel, discipl_specialThisMustRise, spec_requestIType) VALUES 
(1,1,6,5,0,0),
(1,1,107,4,1,0),
(1,2,6,5,0,0),
(1,2,38,4,1,0),
(1,3,6,5,0,0),
(1,3,18,4,1,0),
(5,1,5,5,0,0),
(5,1,113,4,1,0),
(8,1,3,40,1,1),
(8,2,5,40,1,1),
(8,3,4,40,1,1),
(12,1,12,4,1,0),
(31,1,31,4,1,0),
(38,1,114,4,0,0),
(38,1,38,4,1,0),
(54,1,3,50,0,1),
(54,1,54,4,1,0),
(56,1,56,5,0,0),
(56,1,5,5,0,0),
(57,1,57,5,0,0),
(57,1,5,5,1,0),
(63,1,63,5,0,0),
(63,1,113,4,1,0),
(64,1,64,5,0,0),
(64,1,12,4,1,0),
(65,1,65,6,0,0),
(65,1,127,5,1,0),
(65,2,65,6,0,0),
(65,2,12,5,1,0),
(66,1,66,5,0,0),
(66,1,7,4,1,2),
(67,1,67,5,0,0),
(67,1,127,4,1,0),
(67,2,67,5,0,0),
(67,2,12,4,1,0),
(69,1,69,5,0,0),
(69,1,12,4,1,0),
(72,1,72,5,0,0),
(72,1,113,4,1,0),
(75,1,119,5,1,0),
(76,1,119,5,1,0),
(88,1,119,5,1,0),
(89,1,119,5,1,0),
(89,1,129,5,1,0),
(90,1,119,5,1,0),
(90,1,2,5,1,2),
(90,2,119,5,1,0),
(90,2,86,5,1,0),
(90,3,119,5,1,0),
(90,3,9,5,1,2),
(107,1,107,5,1,0),
(110,1,110,4,1,0),
(111,1,111,5,0,0),
(111,1,5,5,1,0),
(112,1,112,4,1,0),
(113,1,113,4,1,0),
(114,1,114,4,1,0),
(115,1,115,4,1,0),
(116,1,116,4,1,0),
(118,1,118,4,1,0),
(119,1,119,4,1,0),
(123,1,123,4,1,0),
(124,1,124,4,1,0),
(126,1,126,4,1,0),
(127,1,127,4,1,0),
(128,1,128,4,1,0),
(129,1,129,6,0,0),
(129,1,143,5,1,0),
(130,1,130,5,1,0),
(132,1,132,5,1,0),
(136,1,136,5,1,0),
(146,1,146,5,1,0)
;
CREATE TABLE chaos_discipline_specAreas (
	discipl_id integer(3),
	spec_areaGroup integer(1),
	spec_areaDescr varchar(45)
);
INSERT INTO chaos_discipline_specAreas (discipl_id, spec_areaGroup, spec_areaDescr) VALUES 
(1,1,"Védekező"),
(1,2,"Támadó"),
(1,3,"Harci esélyjavító"),
(1,4,"Általános esélyjavító"),
(5,1,"Egy adott fajra"),
(8,1,"Hosszútáv futás"),
(8,2,"Sprint"),
(8,3,"Akadályfutás"),
(12,1,"Egy adott terület"),
(31,1,"Fegyverek"),
(31,2,"Páncélok"),
(31,3,"Általános"),
(38,1,"Egy fegyverosztály"),
(54,1,"Egy páncéltípus"),
(56,1,"Egy szituáció vagy elvárás"),
(57,1,"Egy adott fajra"),
(63,1,"Egy adott fajra"),
(64,1,"Egy adott fajra"),
(65,1,"Egy adott tereptípusra"),
(66,1,"Egy tényező és típus alapján"),
(67,1,"Egy adott tereptípusra"),
(69,1,"Egy adott játéktípusra"),
(72,1,"Egy adott fajra"),
(75,1,"Egy varázslattípusra"),
(76,1,"Egy varázslattípusra"),
(88,1,"Mágiaelmélet spec.nak megfelelőre"),
(89,1,"Egy varázslattípusra"),
(90,1,"Egy varázslattípusra"),
(107,1,"Egy fegyvercsoport, ami ellen hatékony"),
(110,1,"Egy adott síkra"),
(111,1,"Egy adott országra"),
(112,1,"Egy adott élőholt csoport"),
(113,1,"Egy adott fajra"),
(114,1,"Fegyver-, páncél-, pajzs-csoportra"),
(115,1,"Egy adott növénycsoport vagy termővidék"),
(116,1,"Egy adott vallás"),
(118,1,"Kultúrkör, ország, vallásra"),
(119,1,"Egy varázslattípusra"),
(123,1,"Tűz, víz, föld, levegő, káosz"),
(124,1,"Egy adott ősvény"),
(126,1,"Egy adott fajra"),
(127,1,"Egy adott tereptípusra"),
(128,1,"Kódextípus-, tárgytípus-csoport"),
(129,1,"Adott terület"),
(130,1,"Adott fajra"),
(132,1,"Adott országra"),
(136,1,"Adott létsíkra"),
(146,1,"Adott helyszím-, kor-, faj-csoportra, stb")
;
CREATE TABLE chaos_discipline_type(
	type_id integer(1) PRIMARY KEY,
	type_name varchar(40),
	type_inheritylBeneficial integer(1)
);
INSERT INTO chaos_discipline_type (type_id, type_name, type_inheritylBeneficial) VALUES 
(1,"Hétköznapok",1),
(2,"Szakmák",1),
(3,"Vér",0),
(4,"Éjszaka",0),
(5,"Esszencia/Pszi",0),
(6,"Védelem",0),
(7,"Ismeret",0),
(8,"Tudomány",0),
(9,"Művészet",0)
;
CREATE TABLE chaos_JPLevels_normal (
	discipl_level integer(2) PRIMARY KEY,
	level_attribJPSchemaAdditioner integer(2),
	level_studyPracticePenalty integer(3),
	discipl_levelAveragePrice integer(4),
	discipl_levelBeneficPrice  integer(4),
	discipl_attribLevelFeedback integer(1)
);
INSERT INTO chaos_JPLevels_normal (discipl_level, 
level_attribJPSchemaAdditioner, level_studyPracticePenalty , 
discipl_levelAveragePrice , discipl_levelBeneficPrice ,  
discipl_attribLevelFeedback)
 VALUES
(1,0,100,200,150,0),
(2,0,100,350,250,0),
(3,0,100,550,400,0),
(4,10,50,900,700,1),
(5,10,50,1200,950,1),
(6,10,50,1550,1250,1),
(7,20,0,1950,1600,2),
(8,20,0,2400,2000,2),
(9,20,0,2900,1450,2),
(10,30,0,3500,3000,3)
;
CREATE TABLE chaos_JPLevels_special(
	discipl_specLevel  integer(2) PRIMARY KEY,
	discipl_levelSpecAveragePrice integer(3),
	discipl_levelSpecBenefPrice integer(3),
	discipl_requirLevelRising integer(1),
	discipl_requirAttribRising integer(2),
	discipl_attribSpecLevelFeedback integer(1)
);
INSERT INTO chaos_JPLevels_special (discipl_specLevel, 
discipl_levelSpecAveragePrice, discipl_levelSpecBenefPrice,
discipl_requirLevelRising, discipl_requirAttribRising,
discipl_attribSpecLevelFeedback
) VALUES 
(1,600,500,0,0,0),
(2,900,750,1,10,1); 
CREATE TABLE chaos_attrib_JPschema (
	JPschema integer(3),
	levelJP_intervalDown integer(3),
	levelJP_intervalUp integer(3),
	levelJP_modifier integer(3)
);
INSERT INTO chaos_attrib_JPschema (JPschema,
levelJP_intervalDown, levelJP_intervalUp, levelJP_modifier) 
VALUES 
(10, 1,9,15),
(10, 10,29,0),
(10, 30,39,-15),
(10, 40,200,-30),
(20, 0,0,100),
(20, 1,9,30),
(20, 10,19,15),
(20, 20,39,0),
(20, 40,49,-15),
(20, 50,200,-30),
(30, 1,9,100),
(30, 10,19,30),
(30, 20,29,15),
(30, 30,49,0),
(30, 50,59,-15),
(30, 60,200,-30),
(40, 1,19,100),
(40, 20,29,30),
(40, 30,39,15),
(40, 40,59,0),
(40, 60,69,-15),
(40, 70,200,-30),
(50, 1,29,100),
(50, 30,39,30),
(50, 40,49,15),
(50, 50,69,0),
(50, 70,79,-15),
(50, 80,200,-30),
(60, 1,39,100),
(60, 40,49,30),
(60, 50,59,15),
(60, 60,79,0),
(60, 80,89,-15),
(60, 90,200,-30),
(70, 1,49,100),
(70, 50,59,30),
(70, 60,69,15),
(70, 70,89,0),
(70, 90,99,-15),
(70, 100,200,-30),
(80, 1,59,100),
(80, 60,69,30),
(80, 70,79,15),
(80, 80,99,0),
(80, 100,109,-15),
(80, 110,200,-30),
(90, 1,69,100),
(90, 70,79,30),
(90, 80,89,15),
(90, 90,109,0),
(90, 110,119,-15),
(90, 120,200,-30),
(100, 1,79,100),
(100, 80,89,30),
(100, 90,99,15),
(100, 100,119,0),
(100, 120,129,-15),
(100, 130,200,-30);
CREATE TABLE chaos_attribute_stats(
	attrib_id integer(2),
	atrib_name varchar(30)
);
INSERT INTO chaos_attribute_stats (attrib_id, atrib_name) VALUES 
(1,"Fizikum"),
(2,"Erő"),
(3,"Szívósság"),
(4,"Rátermettség"),
(5,"Ügyesség"),
(6,"Reflex"),
(7,"Tudat"),
(8,"Inteligencia"),
(9,"Lelkierő"),
(10,"Eszencia"),
(11,"Varázserő"),
(12,"Eszenciapajzs");


CREATE TABLE chaos_raceBase_attribRestraints (
	race_id integer(2) PRIMARY KEY,
	raceName varchar(15),
	physiqueMax integer(3),
	efficiencyMax integer(3),
	conscienceMax integer(3),
	essencyMax integer(3),
	basicSize integer(1)
);
INSERT INTO chaos_raceBase_attribRestraints (race_id, raceName, 
physiqueMax, efficiencyMax, conscienceMax, essencyMax, basicSize) VALUES 
(1,"elf(driád)",80,80,90,90,3),
(2,"elf",70,90,90,90,4),
(3,"ember",80,80,80,90,4),
(4,"gennymanó",50,100,80,70,1),
(5,"gilf",90,90,90,100,4),
(6,"gnóm",60,80,90,80,2),
(7,"gyíklény",70,90,70,70,4),
(8,"manó",50,70,50,70,2),
(9,"myor",100,100,100,100,5),
(10,"ogár",120,70,50,70,5),
(11,"ork",90,70,60,70,4),
(12,"roal",80,80,80,100,4),
(13,"törpe",90,70,80,80,2),
(14,"tündérke",40,100,80,80,1),
(15,"tündérmanó",40,100,80,80,1),
(16,"Elf(driád apa)-ember",75,85,90,85,3),
(17,"Elf-gilf",80,90,90,95,4),
(18,"Elf-gnóm",65,85,90,85,2),
(19,"Elf-manó",60,80,70,80,2),
(20,"Elf-ork",80,80,75,80,4),
(21,"Elf-törpe",80,80,85,85,2),
(22,"Ember-elf",75,85,85,90,4),
(23,"Ember-elf(driád anya)",80,85,85,90,4),
(24,"Ember-elf(najád)",80,85,90,85,4),
(25,"Ember-gilf",85,85,85,95,4),
(26,"Ember-gnóm",70,80,85,85,2),
(27,"Ember-manó",65,75,65,80,2),
(28,"Ember-ogár",100,75,65,80,4),
(29,"Ember-ork",85,75,70,80,4),
(30,"Ember-roal",80,80,80,85,4),
(31,"Ember-törpe",85,75,80,85,2),
(32,"Gilf-manó",70,80,70,85,2),
(33,"Gilf-ork",90,80,75,85,4),
(34,"Gnóm-manó",55,75,70,75,2),
(35,"Gnóm-ogár",80,75,70,75,4),
(36,"Gnóm-ork",75,75,75,75,2),
(37,"Gnóm-törpe",75,75,85,80,2),
(38,"Manó-ogár",85,70,50,70,4),
(39,"Manó-ork",70,70,55,70,2),
(40,"Manó-törpe",70,70,65,75,2),
(41,"Ogár-Ork",105,70,55,70,4)
;
CREATE TABLE chaos_raceSizes(
	basicSize_id integer(1) PRIMARY KEY,
	sizeDescr varchar(20),
	speedQuicknessBasic integer(2)
);
INSERT INTO chaos_raceSizes (basicSize_id, sizeDescr, speedQuicknessBasic) VALUES 
(1,"apró",4),
(2,"kicsi",5),
(3,"közepes",5),
(4,"közepes",6),
(5,"nagy",8),
(6,"Hatalmas",10)
;




CREATE TABLE character_trunk (
	character_id integer(3) PRIMARY KEY,
	race_id integer(2),
	race_name varchar(15),
	character_name varchar(30),
	character_descr varchar(200),
	beneficial_discipl integer(1),
	character_realSize integer(2),
	character_textSize varchar(20),
	character_realSpeed integer(2),
	character_starterJP integer(6),
	lastupdate_timestamp datetime
);
INSERT INTO character_trunk (character_id, race_id, race_name, character_name, character_descr, beneficial_discipl,
	character_realSize, character_textSize, character_realSpeed, character_starterJP, lastupdate_timestamp) VALUES 
	(1,4,"gennymanó","TesztElek", "Tesztelésből készült", 3, 2, "kicsi",  5, 10000, CURDATE()),
	(2,1,"elf(driád)","TesztElena", "Tesztnek készül", 2, 3, "közepes", 4, 21000, CURDATE());

CREATE TABLE character_collectJP (
	jp_index integer(3) PRIMARY KEY,
	character_id integer(3),
	character_collectedJP integer(8)
);
INSERT INTO character_collectJP (character_id, jp_index, character_collectedJP) 
VALUES 
(1,1,12000),
(1,2,1100),
(2,1,2000)
;
CREATE TABLE character_attribStats (
	character_id integer(3) PRIMARY KEY,
	attrib1_basicPhyisique integer(3),
	attrib2_basicStrenght integer(3),
	attrib3_basicStamina integer(3),
	attrib4_basicEfficiency integer(3),
	attrib5_basicDexterity integer(3),
	attrib6_basicReflex integer(3),
	attrib7_basicConscience integer(3),
	attrib8_basicIntegency integer(3),
	attrib9_basicFortitude integer(3),
	attrib10_basicEssence integer(3),
	attrib11_basicMagic integer(3),
	attrib12_basicEssenceshild integer(3)
);
INSERT INTO character_attribStats (character_id,attrib1_basicPhyisique,attrib2_basicStrenght,attrib3_basicStamina,
 attrib4_basicEfficiency,attrib5_basicDexterity,attrib6_basicReflex,attrib7_basicConscience,attrib8_basicIntegency,
 attrib9_basicFortitude,attrib10_basicEssence,attrib11_basicMagic,attrib12_basicEssenceshild) 
VALUES 
(1,60,60,60,60,60,60,60,60,60,60,60,60),
(2,62,63,62,33,65,67,60,61,61,65,63,61);
CREATE TABLE character_chosenDiscipl(
	discipl_index integer(5),
	character_id integer(3),
	discipl_id integer(2),
	discipl_type integer(1),
	discipl_chosenAttrib integer(2),
	discipl_requirNormal integer(1),
	discipl_notation varchar(100)
);
INSERT INTO character_chosenDiscipl (discipl_index, character_id, discipl_id, discipl_type, discipl_chosenAttrib, 
discipl_requirNormal, discipl_notation) 
VALUES 
(1,1,31,2,2,0,"valami"),
(1,2,54,3,3,0,"még valami");
CREATE TABLE character_chosenLevel(
	discipl_index integer(5),
	character_id integer(3),
	discipl_level integer(2),
	discipl_levelFinalJP integer(4),
	discipl_metorated integer(1),
	discipl_hasThereAttrPointFromThis integer(1),
	discipl_isThisPointSpent integer(4)
);
INSERT INTO character_chosenLevel (discipl_index, character_id, discipl_level, discipl_levelFinalJP, 
discipl_metorated, discipl_hasThereAttrPointFromThis, discipl_isThisPointSpent) 
VALUES 
(1,1,1,700,0,0,0),
(1,1,2,900,1,0,0),
(1,1,3,700,0,0,0),
(1,1,4,900,1,1,1),
(1,1,5,700,0,1,2),
(1,1,6,900,1,1,3),
(2,2,1,500,1,0,0);
CREATE TABLE character_chosenSpec(
	discipl_index integer(5),
	character_id integer(3),
	discipl_specIndex integer(2),
	discipl_requirSpec integer(1),
	discipl_specialAreaGroup integer(1),
	discipl_specialDescr varchar(100),
	discipl_level integer(2),
	discipl_levelFinalJP integer(4),
	discipl_isThisPointSpent integer(4)
);
INSERT INTO character_chosenSpec (discipl_index, character_id, discipl_specIndex, discipl_requirSpec, discipl_specialAreaGroup,
 discipl_specialDescr, discipl_level, discipl_levelFinalJP, discipl_isThisPointSpent) 
VALUES 
(1,1,1,2,2,"me mért ne",1,400,0),
(1,1,1,2,2,"me mért ne",2,900,4);
CREATE TABLE character_attribEnchance (
	attrib_echanceIndex integer(4),
	character_id integer(3),
	discipl_index1 integer(3),
	discipl_index2 integer(3),
	attrib_id integer(2),
	discipl_pointFeedbackType integer(1)
);
INSERT INTO character_attribEnchance (character_id, attrib_echanceIndex, discipl_index1,
	discipl_index2, attrib_id, discipl_pointFeedbackType) 
VALUES 
(1,1,1,0,2,1),
(1,2,1,0,2,1),
(1,3,1,0,2,1),
(1,4,1,0,2,1);