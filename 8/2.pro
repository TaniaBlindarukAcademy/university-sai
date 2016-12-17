domains 
	str=string
	lstr=string*
	int=integer
predicates
	nondeterm family (str,str,str,lstr)
	nondeterm member(str,lstr)
	nondeterm length(lstr, int)
	qw1
	qw2
clauses
	family("Petrov","Petr","Masha",["Pasha","Sasha","Misha"]).
	family("Sidorov","Sidr","Ksysha",["Dasha","Kesha","Misha"]).
	family("Ivanov","Ivan","Ivanka",["Vanya"]).
	family("Antonov","Anton","Gadya",[]).
	family("Vadimovich","Vadim","Alisa",["March_Hare"]).
	family("Yoshi","Hamato","Splinter",["Donatelo","Rafael","Mikelandgelo","Leonardo"]).
	family("Spasateli","Rockfor","Gaika",["Chip","Deil","Vgik"]).
	family("Dota","Icefrog","Gaben",["Monkey_King","Balans"]).
	family("Kirigaya","Kazuto","Asuna",["Ui"]).
	family("Namikadze","Minato","Kushina",["Naruto"]).
	member (X, [X|_]).
	member (X,[_|Y]):-
		member (X,Y).
	length ([],0).
	length ([_|S],N):-
		length (S,N1),
		N=N1+1.
	qw1:-write ("Wedite chislo detey"),
		readint (K),
		family (F,_,_,D),
		length (D,N),
		N>K,
		write(F),nl, fail.
	qw1:-write("Semey bolshe net"),nl.
	qw2:-write ("Wedite imya rebenka"),
		readln(X),
		family (F,_,_,D),
		member(X,D),
		write (F),nl,fail.
	qw2:-write("Semey bolshe net"),nl.
Goal
	qw1,qw2.	
	
	