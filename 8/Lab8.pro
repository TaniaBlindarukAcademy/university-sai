domains
	str = string
	list_str = string*
	database-ss
	b(str, str)
	
predicates
	nondeterm family(str, str, str, list_str)
	nondeterm m(integer)
	nondeterm menu
	nondeterm length(list_str, integer)
	nondeterm member(list_str, str, str)
clauses
	family("Ivanov", "Jon", "Marry", ["Nick", "Olga"]).
	family("Petrov", "Martin", "Marry", []).
	family("Bobrov", "Oleg", "Marry", ["Nick", "Vova", "Stephan", "Victor"]).
	family("Skywalker", "Ivan", "Marry", ["Lui", "Olga"]).
	family("Doomsday", "Petro", "Marry", ["Petro", "Marry", "Julia"]).
	family("Batman", "Ashot", "Marry", ["Oksana", "Tatiana", "Stacy"]).
	family("Absalom", "Artem", "Marry", ["Alex", "Jean", "Omar"]).
	family("Simpson", "Oleksandr", "Marry", ["Bart", "Liza"]).
	family("Petriv", "Simon", "Marry", []).
	family("Barak", "Obama", "Marry", ["Black", "True", "Nigga"]).
	family("Putin", "Vova", "Marry", ["Petro", "Nick", "Oleg", "Olga", "Vaselyna"]).
	family("Madden", "Steve", "Marry", ["Right", "Left"]).
	family("Memchick", "Arkadiy", "Marry", ["Nike", "Addidas", "Puma"]).
	family("Nebuyanov", "Ostap", "Marry", ["Child-1", "Child-2", "Child-3"]).
	family("Bandera", "Stephan", "Marry", ["Glory", "To", "Ukraine", "Heroes", "Fame"]).
	member([H|T], Name, Family):-
		H=Name,write(Family), nl;
		member(T, Name, Family).
	member([],_, _).
	
	length([_|T], Y):- 
		length(T, Y1),Y=Y1 + 1.
	length([],0).
	
	
	menu:- write(" 1-Find family without children"),nl,
		write(" 2-Find family with more than K"),nl,
		write(" 3-Find family which have from K to L children. "),nl,
		write(" 4-Find family by child"),nl, 
		readint(X),
		m(X).
		m(1):-
			family(S, _, _, L),
			length(L, C),
			C = 0, write(S),nl,
			fail.
		m(1):-
			write("End of search."),nl,
			menu.
		m(2):-
			write("Enter K:"),nl,
			readint(K),
			family(S, _, _, L),
			length(L, C),
			C > K, write(S), nl,
			fail.
		m(2):-
			write("End of search."),nl,
			menu.
		m(3):- 
			write("Enter K:"), nl,
			readint(K),
			write("Enter L:"),nl,
			readint(L),
			family(S,_,_,List),
			length(List,C),
			C >= K, C <= L, write(S," ", C), nl,
			fail.
		m(3):- 
			write("End of search."), nl,
			menu.
		m(4):-
			write("Enter name of the child:"),nl,
			readln(X),
			family(S, _,_,L),
			member(L, X, S),
			fail.
		m(4):-
			menu.
goal 
menu.