This project is created using the Visual Studio with .Net Core 3.1.

To run this project use visual studio.

Input file is included inside the project with inputs.

Input file:
	-First line contains the project list with comma separated
	-Second line contains the project dependencies.

Output is the order of build of projects with Depenencies.

Project is teste with the following inputs.

Please provide single input at a time.

Input 1:
P1, P2, P3, P4
(P1, P2), (P1, P3), (P3, P4) 

Output: 
Projects list is P1, P2, P3, P4
Project dependencies(P1, P2), (P1, P3), (P3, P4)
Order of the Project Build is P2>>P4>>P3>>P1

Input 2:
P1,P2,P3,P4,P5
(P3,P4),(P4,P5),(P1,P2),(P2,P3)

Output:
Projects list is P1,P2,P3,P4,P5
Project dependencies(P3,P4),(P4,P5),(P1,P2),(P2,P3)
Order of the Project Build is P5>>P4>>P3>>P2>>P1
 
Input3:
P1, P2, P3, P4
(P3, P4), (P1, P3), (P1, P2)

Output:
Projects list is P1, P2, P3, P4
Project dependencies(P3, P4), (P1, P3), (P1, P2)
Order of the Project Build is P4>>P3>>P2>>P1

Input 4
P1,P2,P3,P4,P5
(P2,P1),(P3,P2),(P3,P4),(P5,P4)

Output:
Projects list is P1,P2,P3,P4,P5
Project dependencies(P2,P1),(P3,P2),(P3,P4),(P5,P4)
Order of the Project Build is P1>>P2>>P4>>P3>>P5

Input 5
P1,P2,P3
(P1,P2),(P2,P3),(P3,P1)

Output:
Projects list is P1,P2,P3
Project dependencies(P1,P2),(P2,P3),(P3,P1)
Order of the Project Build is P3>>P2>>P1


for incorrect input format

Input:
P1 P2 P3
[P1,P2],[P2,P3],(P3,P1

Output:
Project list is not comma seperated