DOMAINS
	i=integer
	s=string
	c=char
	li=i*
DATABASE - knowledge
	cond(i,s)
	rule(s,li)
DATABASE - dialog
	cond_is(i,c)
PREDICATES
	start
	animals
	check(li)
	test_cond(i)
	update
	add_cond(li)
	print_cond(i,li,li)
	read_cond(c,li,li)
	ex_cond(i,li,li,i)
	wr_cond(c,i,li,li)
	new_cond(i,li,li)
	read_true_char(c)
	test(c,c)
CLAUSES
	start:-
		consult("A:\animals.ddb",knowledge),
		write("Zagadajte zivotnoe, a ja popitajus' ego otgadat'"),nl,
		animals,
		retractall(_,dialog),
		retractall(_,knowledge),
		nl,nl,write("Hotite esche raz sygrat'? (1 - da, 2 - net)"),
		read_true_char(C),
		C='1',!,start.
	start:-
		nl,nl,write("Vsego dobrogo! Do novyh vstrech"),
		readchar(_).
	animals:-
		rule(X,L),
		check(L),
		nl,write("Ja dumaju, eto ",X),
		nl,write("Ja prav? (1 - da, 2 - net)"),
		read_true_char(C),C='1',!.
	animals:-
		nl,write("Ja ne znaju, chto eto za zhivotnoe"),nl,
		nl,write("Davajte dobavim ego v moju bazu znanij"),nl,
		update.
	update:-
		nl,write("Vvedite nazvanie zhivotnogo: "),
		readln(S),
		add_cond(L),
		assert(rule(S,L),knowledge),
		save("animals.ddb",knowledge).
	add_cond(L):-
		cond_is(_,'1'),!,
		nl,write("O nem izvestno, chto ono: "),
		print_cond(1,[],L1),
		nl,write("Izvestno li o nem esche chto-nibud'? (1 - da, 2 - net)"),
		read_true_char(C),
		read_cond(C,L1,L).
	add_cond(L):-
		read_cond('1',[],L).
	print_cond(H,L,L):-
		not(cond(H,_)),!.
	print_cond(H,L,L1):-
		cond_is(H,'1'),!,
		cond(H,T),
		H1=H+1,
		nl,write(T),
		print_cond(H1,[H|L],L1).
	print_cond(H,L,L1):-
		H1=H+1,
		print_cond(H1,L,L1).
	read_cond('1',L,L2):-
		ex_cond(1,L,L1,N),
		new_cond(N,L1,L2),!.
	read_cond(_,L,L):-!.
	ex_cond(N,L,L,N):-
		not(cond(N,_)),!.
	ex_cond(N,L,L1,N2):-
		cond_is(N,_),!,
		N1=N+1,
		ex_cond(N1,L,L1,N2).
	ex_cond(N,L,L1,N2):-
		cond(N,S),
		nl,write("Ono ",S,"? (1 - da, 2 - net)"),
		read_true_char(A),
		wr_cond(A,N,L,L2),
		N1=N+1,
		ex_cond(N1,L2,L1,N2).
	wr_cond('1',N,L,[N|L]):-!.
	wr_cond('2',_,L,L):-!.
	new_cond(N,L,L1):-
		nl,write("Est' esche svojstva? (1 - da, 2 - net)"),
		read_true_char(A),
		A='1',!,
		nl,write("Ukazhite novoe svojstvo, kotorym obladaet zhyvotnoe"),
		nl,write("v vide 'ono <opisanie novogo svojstva>'"),readln(S),
		assert(cond(N,S)),
		N1=N+1,
		new_cond(N1,[N|L],L1).
	new_cond(_,L,L).
	check( [H|T] ):-
		test_cond(H),
		check(T).
	check([]).
	test_cond(H):-
		cond_is(H,'1'),!.
	test_cond(H):-
		cond_is(H,'2'),!,
		fail.
	test_cond(H):-
		cond(H,S),
		nl,write("Ono ",S,"? (1 - da, 2 - net)"),
		read_true_char(A),
		assert(cond_is(H,A)),
		test_cond(H).
	read_true_char(C):-
		readchar(C1),
		test(C1,C).
	test(C,C):-
		'1'<=C,C<='2',!.
	test(_,C):-
		write("Nazhmite 1 ili 2!"),nl,
		readchar(C1),
		test(C1,C).
GOAL
	start