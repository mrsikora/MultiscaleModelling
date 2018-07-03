# MultiscaleModelling
Grain growth implemented by using Cellular Automata method. Implemented using C#.

A cellular automata is a collection of colored cells on a grid. It come in a variety of shapes and varieties depend of rules based on the states of neighboring cells. Those rules are applied iteratively for as many time steps as desired to fill the whole grid. It is possible to achieve different simulations and results by using various rules.

Cells have clearly defined interaction rules between each other. It was used Moore neighborhood and periodic boundary conditions.
Implemented features:
- change size of microstructure by using width- and height- UpDown field on the GUI;
- set number of grains;
- possibility to import/export structure to bitmap or text file. User have the opportunity (DialogBox) to choose where to save the file or which file import;
- add inclusions and choose type: circular with radious or square with dimeter. It is also possible to add circular or square inclusions after simulation. Inclusions are added on grain boundaries. User has also ability to set amount of inclusions;
- added 4 rules – the first for Moore neighbors (if five to eight of the cells neighbors id’s is equal to S, then cell transforms to the state S), the second for nearest Moore neighbors (if three of the cells neighbors id’s is equal to S, then cell transforms to the state S) and the third for further Moore neigbors (if three of the cells neighbors id’s is equal to S, then cell transforms to the state S). The fourth rule determined by global probability of change and it was Moore neighborhoods;
- added different microstructure type: substructure and dual phase. It is possible to select grains (selected grains not growth in second simulations);
- grain boundary selection. It is possible to set size of GB and add boundary to selected grains. Moreover, the percentage of GB is calculated;
- added Monte Carlo graing growth algorytm and Recrystallization in mictrosctructure (added states to cells – cells in the same state represent particular grain, added „Number of states” field and MC steps” field to set number of iterations);
- possibility to add dual phase mictrostructure;
- added heterogeneous or homogeneous energy distribution and visualization of the energy after hover a button. User can set an energy on endges and inside;
- possibility to nucleation: anywhere or on grain boundaries. The user can also add nucleons on the beginning of the simulation(e.g. 100) or Increasing nucleation rate (e.g. 10,20,30,40,..);
- MC static recrystallization algorithm – growth;
