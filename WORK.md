insert into RaceSchedule
select distinct 
       meet Location, 
		 rcDate RaceDate, 
		 rcNo RaceNumber,
		 '종료'
  from RACE_RESULT;

insert into RaceResult
select distinct 
       meet Location, 
		 rcDate RaceDate, 
		 rcNo RaceNumber,
		 rcChul TrackNumber, 
		 hrno HorseNumber, 
		 jkName RiderNumber,
		 rcOrd Rank 
  from RACE_RESULT;