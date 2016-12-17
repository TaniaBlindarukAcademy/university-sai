domains
             s=string
  database-ss
             b(s,s) 

    predicates
              
           nondeterm  parent(s,s)
            nondeterm man(s)
            nondeterm woman(s)
            nondeterm father(s,s)
            nondeterm mother(s,s)
            nondeterm son(s,s)
            nondeterm doughter(s,s)
            nondeterm g_father(s,s)
            nondeterm g_mother(s,s)
            nondeterm g_son(s,s)
            nondeterm g_doughter(s,s)
            nondeterm brother(s,s)
            nondeterm sister(s,s)
            nondeterm is_brother(s,s)
            nondeterm dob(s,s) 
            nondeterm menu
            nondeterm m(integer)   

      clauses 

             parent("Ivan","Pet").
             parent("Ivan","Nic").
             parent("Ivan","Olga").
             parent("Mary","Pet").
             parent("Mary","Nic").
             parent("Mary","Olga").
            parent("Pet","Stepan").
             parent("Pet","Nady").
             parent("Nina","Stepan").
             parent("Nina","Nady").
             parent("Nic","Tany").
             parent("Nic","Lena").
            parent("Lucy","Tany").
             parent("Lucy","Lena").
             parent("Olga","Vasya").
             parent("Olga","Serg").
             parent("Olga","Kostya").
             parent("Jon","Vasya").
             parent("Jon","Serg").
             parent("Jon","Kostya").

             man("Ivan").
             man("Pet").
             man("Nic").
             man("Jon").
             man("Stepan").
             man("Vasya").
             man("Serg").
             man("Kostya").

             woman("Mary").
             woman("Nina").
             woman("Lucy").
             woman("Olga").
             woman("Nady").
             woman("Tany").
             woman("Lena").
             

            father(X,Y):- parent(X,Y), man(X).

            mother(X,Y):- parent(X,Y), woman(X).

            son(X,Y):- parent(Y,X), man(X).

            doughter(X,Y):-parent(Y,X), woman(X).

            g_father(X,Y):-parent(X,Z), parent(Z,Y), man(X).

            g_mother(X,Y):-parent(X,Z), parent(Z,Y), woman(X).

            g_son(X,Y):-parent(Z,X), parent(Y,Z), man(X).

            g_doughter(X,Y):-parent(Z,X), parent(Y,Z), woman(X).

            brother(X,Y):- parent(Z,X), parent(Z,Y), man(X),X<>Y.

            sister(X,Y):-parent(Z,X), parent(Z,Y), woman(X),X<>Y.

           is_brother(X,Y):-brother(X1,Y1),dob(X1,Y1),fail;

                              b(X,Y).

             dob(X,Y):-b(X,Y),!;

                       assert(b(X,Y)).  
             
             
             menu:-   write("   1-Found father"),nl,
                      write("   2-Found mother"),nl,
                      write("   3-Found son"),nl,
                      write("   4-Found brother"),nl,
                      write("   5-Found sister"),nl,
                      write("   6-Found doughter"),nl,
                      write("   7-Found g_father"),nl,
                      write("   8-Found g_mother"),nl,
                      write("   9-Found g_son"),nl,
                      write("   10-Found g_doughter"),nl,
                      
                      readint(X),
                      
                      m(X).
                      
                         m(1):-write("   Input name the man"),nl,
                               readln(X),
                               father(X,Y),
                               write(" The "),
                               write(X),
                               write(" father ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                               
                         m(2):-write("   Input name the woman"),nl,
                               readln(X),
                               mother(X,Y),
                               write(" The "),
                               write(X),
                               write(" mother ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(3):-write("   Input name the man"),nl,
                               readln(X),
                               son(X,Y),
                               write(" The "),
                               write(X),
                               write(" son ",Y), nl,
                               fail; 
                               readln(_),
                               menu.
                      m(4):-write("   Input name the man"),nl,
                               readln(X),
                               brother(X,Y),
                               write(" The "),
                               write(X),
                               write(" brother ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(5):-write("   Input name the woman"),nl,
                               readln(X),
                               sister(X,Y),
                               write(" The "),
                               write(X),
                               write(" sister ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(6):-write("   Input name the doughter"),nl,
                               readln(X),
                               doughter(X,Y),
                               write(" The "),
                               write(X),
                               write(" doughter ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(7):-write("   Input name the man"),nl,
                               readln(X),
                               g_father(X,Y),
                               write(" The "),
                               write(X),
                               write(" g_father ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(8):-write("   Input name the woman"),nl,
                               readln(X),
                               g_mother(X,Y),
                               write(" The "),
                               write(X),
                               write(" g_mother ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(9):-write("   Input name the man"),nl,
                               readln(X),
                               g_son(X,Y),
                               write(" The "),
                               write(X),
                               write(" g_son ",Y), nl,
                               fail;
                               readln(_),
                               menu.
                      m(10):-write("   Input name the woman"),nl,
                               readln(X),
                               g_doughter(X,Y),
                               write(" The "),
                               write(X),
                               write(" g_doughter ",Y), nl,
                               fail;
                               readln(_),
                               menu.
     goal
     menu.