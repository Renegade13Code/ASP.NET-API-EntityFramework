INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES ('b950ddf5-e9ad-47ff-9d2a-57259014fae6', 'NRTHL', 'Northland Region', 13789, -35.3708304, 172.5717825, 194600);
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES ('907f54ba-2142-4719-aef9-6230c23bd7de', 'AUCK', 'Auckland Region', 4894, -36.5253207, 173.7785704, 1718982);
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES ('79e9872d-5a2f-413e-ac36-511036ccd3d4', 'WAIK', 'Waikato Region', 8970, -37.5144584, 174.5405128, 496700);
INSERT INTO Regions (Id, Code, Name, Area, Lat, Long, Population) 
VALUES ('68c2ab66-c5eb-40b6-81e0-954456d06bba', 'BAYP', 'Bay Of Plenty Region', 12230, -37.5328259, 175.7642701, 345400);



INSERT INTO WalkDifficulty (Id, Code)
VALUES ('4c2b95e0-2022-4a8f-b537-eb3a32786b06', 'Easy');
INSERT INTO WalkDifficulty (Id, Code)
VALUES ('a1066e97-c7c8-4aee-905b-61bb31d82bfb', 'Medium');
INSERT INTO WalkDifficulty (Id, Code)
VALUES ('30f96ef9-7b45-42f7-9fab-05a70e7fc394', 'Hard');



INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES ('b950ddf5-e9ad-47ff-9d2a-57259014fae6', 'Waiotemarama Loop Track', 1.5 , 'a1066e97-c7c8-4aee-905b-61bb31d82bfb', 'b950ddf5-e9ad-47ff-9d2a-57259014fae6');
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES ('907f54ba-2142-4719-aef9-6230c23bd7de', 'Mt Eden Volcano Walk', 2 , '4c2b95e0-2022-4a8f-b537-eb3a32786b06', '907f54ba-2142-4719-aef9-6230c23bd7de');
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES ('79e9872d-5a2f-413e-ac36-511036ccd3d4', 'One Tree Hill Walk', 3.5 , '4c2b95e0-2022-4a8f-b537-eb3a32786b06', '907f54ba-2142-4719-aef9-6230c23bd7de');
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES ('68c2ab66-c5eb-40b6-81e0-954456d06bba', 'Lonely Bay', 1.2 , '4c2b95e0-2022-4a8f-b537-eb3a32786b06', '79e9872d-5a2f-413e-ac36-511036ccd3d4');
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES ('4c2b95e0-2022-4a8f-b537-eb3a32786b06', 'Mt Te Aroha To Wharawhara Track Walk', 32 , '30f96ef9-7b45-42f7-9fab-05a70e7fc394', '68c2ab66-c5eb-40b6-81e0-954456d06bba');
INSERT INTO Walks (Id, Name, Length, WalkDifficultyId, RegionId)
VALUES ('a1066e97-c7c8-4aee-905b-61bb31d82bfb', 'Rainbow Mountain Reserve Walk', 3.5 , 'a1066e97-c7c8-4aee-905b-61bb31d82bfb', '68c2ab66-c5eb-40b6-81e0-954456d06bba');
