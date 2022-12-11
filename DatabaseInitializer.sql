create table if not exists Team(
	Id integer primary key,
	Name text not null
);

create table if not exists Player(
	Id integer primary key,
	Name text not null,
	Age integer not null,
	Height real not null,
	TeamId integer not null,
	foreign key (TeamId)
		references Team(Id)
			on delete restrict
			on update restrict 
);

---------

insert into Team(Id, Name) values(1, 'Inter Milan'), (2, 'Manchester United');

insert into Player(Id, Name, Age, Height, TeamId)
	values(1, 'Samir Handanovic', 38, 1.93, 1)
	      (2, 'Milan Skriniar', 27, 1.88, 1),
	      (3, 'Danilo D''Ambrosio', 34, 1.81, 1),	
	      (4, 'Marcelo Brozovic', 30, 1.81, 1),	
	      (5, 'Lautaro Martinez', 25, 1.74, 1),
	      (6, 'David de Gea', 32, 1.89, 2),
	      (7, 'Phil Jones', 30, 1.85, 2),
	      (8, 'Harry Maguire', 29, 1.94, 2),
	      (9, 'Donny van de Beek', 25, 1.84, 2),
	      (10, 'Mason Greenwood', 21, 1.81, 2);