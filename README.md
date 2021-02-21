

## Mars Rover Project

You can use specific classes, enums, etc. to help you understand the project. I would like to tell you about the structures.

**Rover.cs** is the main file the project and includes a few methods.

### Methods

**MainLogic**
It is the main function that controls all the operations that Rover will perform on Mars. It runs all intermediate functions within it. 

These functions are;

 1. Separating the input into lines. 

 2. Convert the non-numerical values in the first row to numerical values and create the limits.
    
 3. Converting incoming non-numerical rover coordinates to numerical
        rover coordinates. (Using Direction enum)
        
 4. Calculating the target point and checking whether the limit is exceeded in this process. 

 5. Converting the calculated target point back to non-numeric output as
    desired.

 6. To do all the above functions in a loop.

**ConvertToResult**
Converts the numerical result values to the results requested from us and non-numerical. 

For example: 

    Numerical output: 1 3 0 
    Non-numeric and requested output from us: 1 3 N

**ConvertToCoordinates**
It makes non-numerical coordinate inputs numerical in order to perform the necessary operations. Here it makes use of the Direction enum.

For Example:

     Non-numerical input: 1 2 N 
     Numerical version: 1 2 0
**CreateBorder**

It is the function that takes the coordinates of the boundaries from the first line of the input to numeric and record them in order to ensure that Rover does not go beyond the plateau and generate the necessary warnings.

**IsLimit**

The isLimit is a function that checks whether the rover has crossed the plateau boundaries and prevents it from p	assing. It is used in GetRoute function.

Returns:

    reachedExceededLimit { minx, maxx, miny, maxy }

### Enums
#### DirectionHeading
These enums are for manipulations on values in the coordinate plane. For example, if the M command comes while the direction is in the N direction; 

       If we are to call the coordinates [x, y, N], the new coordinate becomes [x, y + 1, N].

#### Heading

 The necessary explanation is made in the Direction enum section.
 #### Direction
 The heading part is the part where the commands that determine the rover's actions are listed. 

For example, when a rover in the N direction receives the L command, it takes the W direction. In order to represent these directions numerically, from 0 to 3 in the clockwise direction, they are listed as N-E-S-W. These numerical values represent the 3rd element in the 3-element vector indicating the coordinates and direction of Rover. 

Also, the 3rd element in this vector is always valued in mode 4. For example, when it is 4, it automatically turns to 0. Or when it is -1, it automatically returns to 3.
