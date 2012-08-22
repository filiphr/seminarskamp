INSERT INTO Ocena(Ocena)
VALUES (5);

INSERT INTO Ocena(Ocena)
VALUES (6);

INSERT INTO Ocena(Ocena)
VALUES (7);

INSERT INTO Ocena(Ocena)
VALUES (8);

INSERT INTO Ocena(Ocena)
VALUES (9);

INSERT INTO Ocena(Ocena)
VALUES (10);



INSERT INTO Predmet(Ime)		
VALUES ('Mikroprocesorski Sistemi');

INSERT INTO Predmet(Ime)		
VALUES ('Sistemski Softver');

INSERT INTO Predmet(Ime)		
VALUES ('Prepoznavanje na Oblici');

INSERT INTO Predmet(Ime)		
VALUES ('Elektronska i Mobilna Komercija');

INSERT INTO Predmet(Ime)		
VALUES ('Korisnicki Interfejsi');

INSERT INTO Predmet(Ime)		
VALUES ('Podatocno Rudarenje');

INSERT INTO Predmet(Ime)		
VALUES ('Digitalen Prenos na Informacii');


INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Dimitar', 'Trajanov');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Riste', 'Stojanov');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Milos', 'Jovanovikj');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Sonja', 'Filiposka');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Slobodan', 'Kalajdziski');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Kire', 'Trivodaliev');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Aristotel', 'Tentov');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Goce', 'Dokoski');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Dejan', 'Gjorgjevikj');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Gjorgji', 'Madjarov');

INSERT INTO NastavenKadar(Ime, Prezime)		
VALUES ('Marija', 'Kalendar');


INSERT INTO Student(Indeks, Ime, Prezime)		
VALUES ('63/2009', 'Filip', 'Hrisafov');

INSERT INTO Student(Indeks, Ime, Prezime)		
VALUES ('199/2009', 'Paulina', 'Grnarova');

INSERT INTO Student(Indeks, Ime, Prezime)		
VALUES ('287/2009', 'Natasa', 'Grnarova');

INSERT INTO Student(Indeks, Ime, Prezime)		
VALUES ('99/2009', 'Bojan', 'Najdenov');

INSERT INTO Student(Indeks, Ime, Prezime)		
VALUES ('357/2009', 'Vasil', 'Stardelov');


INSERT INTO Uslov(Ime)
VALUES ('I_Kolokvium_Usno');

INSERT INTO Uslov(Ime)
VALUES ('II_Kolokvium_Usno');

INSERT INTO Uslov(Ime)
VALUES ('I_Kolokvium_Pismeno');

INSERT INTO Uslov(Ime)
VALUES ('II_Kolokvium_Pismeno');

INSERT INTO Uslov(Ime)
VALUES ('Ispit_Pismeno');

INSERT INTO Uslov(Ime)
VALUES ('Ispit_Usno');

INSERT INTO Uslov(Ime)
VALUES ('Laboratoriski');

INSERT INTO Uslov(Ime)
VALUES ('Seminarska');

INSERT INTO Uslov(Ime)
VALUES ('Prisustvo');

INSERT INTO Uslov(Ime)
VALUES ('Domasna');

INSERT INTO Uslov(Ime)
VALUES ('Testovi');


INSERT INTO Skala(predmet_kod, ocena_ocena, Min, Maks)
VALUES (3, 5, 0, 59);

INSERT INTO Skala(predmet_kod, ocena_ocena, Min, Maks)
VALUES (3, 6, 60, 67);

INSERT INTO Skala(predmet_kod, ocena_ocena, Min, Maks)
VALUES (3, 7, 68, 75);

INSERT INTO Skala(predmet_kod, ocena_ocena, Min, Maks)
VALUES (3, 8, 76, 83);

INSERT INTO Skala(predmet_kod, ocena_ocena, Min, Maks)
VALUES (3, 9, 84, 91);

INSERT INTO Skala(predmet_kod, ocena_ocena, Min, Maks)
VALUES (3, 10, 91, 100);


INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('I_Kolokvium_Usno' , 3, 15, 15);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('II_Kolokvium_Usno' , 3, 15, 15);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('I_Kolokvium_Pismeno' , 3, 10, 10);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('II_Kolokvium_Pismeno' , 3, 10, 10);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Ispit_Pismeno' , 3, 20, 20);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Ispit_Usno' , 3, 30, 30);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Laboratoriski' , 3, 0, 10);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Domasna' , 3, 0, 5);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Testovi' , 3, 0, 0);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Prisustvo' , 3, 0, 5);

INSERT INTO Predmet_Uslov(uslov_ime, predmet_kod, Min_Procent, Procent)
VALUES ('Seminarska' , 3, 15, 30);

