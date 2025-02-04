# dsm
- Stands for "**d**o **s**o**m**ething"
- Programming language
- Interpreted
- Whitespace is ignored

## Requirements
.NET 9.0 Runtime

## Installation
```
git clone https://github.com/bf4r/dsm
cd dsm
```

## Usage
```
dotnet run <your code here>
```

## Example
Hello, World! in dsm:
```
cws Hello. cws ,. cwu 32. cws World. cwlu 33.
```
### So...
```
dotnet run cws Hello. cws ,. cwu 32. cws World. cwlu 33.
```
Please refer to the instruction map for all instructions, their example usage and a description of what they do.

# instruction map
## d
#### df
- Define function
- Creates a new function that can be called later.
`df myfunction: cwls hello%e.`
- Replace all dots in the function code with %e, as in end.
#### dvs
- Define variable string
- Creates a new string variable which can be used or redefined later.
`dvs mystring, hello.`
#### dvi
- Define variable int
- Creates a new integer variable which can be used or redefined later.
`dvi myint, 1.`
##### dvfis
- Define variable from int -> string
- Converts an existing integer variable to a string variable, keeping the integer variable but creating a new string variable with a text representation of the integer variable's value.
```
dvi myint, 1.
dvfis mystring, myint.
```
##### dvfsi
- Define variable from string -> int
- Converts an existing string variable to an integer variable, keeping the string variable but creating a new integer variable with a numeric value that matches the string representation of that value.
- Only assigns the value if the string value is a valid representation of a whole number.
```
dvs mystringnumber, 123.
dvfsi myint, mystringnumber.
```
## m
#### mviavi
- Modify variable int add variable int
- Adds the value of the second integer variable to the value of the first one.

```
dvi a, 1.
dvi b, 2.
mviavi a, b.
```
a = 3 now (1+2).
#### mvisvi
- Modify variable int subtract variable int
- Subtracts the value of the second integer variable from the value of the first one and sets the first one to the result.
```
dvi a, 2.
dvi b, 1.
mvisvi a, b.
```
a = 1 now (2-1).
## x
#### xvs
- Delete variable string
- Deletes a string variable from the list of currently string variables.
```
dvs mystring, hello.
xvs mystring.
```
#### xvi
- Delete variable int
- Deletes an integer variable from the list of currently registered integer variables.
```
dvi myint, 1.
xvi myint.
```
## c
### cc
- Console clear
- Clears the console.
`cc`
#### cwu
- Console write UTF-8
- Writes the ASCII or Unicode value represented by the input int to the standard output.
`cwu 97.`
This prints a without a new line.
#### cwlu
- Console write line UTF-8
- Writes the ASCII or Unicode value represented by the input int to the standard output.
- Adds a newline at the end.
`cwlu 97.`
This prints a and a new line.
#### cwvi
- Console write variable int
- Writes the value of an integer variable to the standard output.
```
dvi myint, 1.
cwvi myint.
```
#### cwlvi
- Console write line variable int
- Writes the value of an integer variable to the standard output.
- Adds a newline at the end.
```
dvi myint, 1.
cwlvi myint.
```
#### cwvs
- Console write variable string
- Writes the value of a string variable to the standard output.
```
dvs mystring, hello.
cwvs mystring.
```
#### cwlvs
- Console write line variable string
- Adds a newline at the end.
```
dvs mystring, hello.
cwlvs mystring.
```
#### cws
- Console write string
- Writes a literal string to the standard output. Only supports single words as all whitespace is stripped out in the interpretation process.
`cws hello.`
#### cwls
- Console write line string
- Console write string
- Writes a literal string to the standard output. Only supports single words as all whitespace is stripped out in the interpretation process.
- Adds a newline at the end.
`cwls hello.`
## e
#### ef
- Execute function
- Executes a defined function.
```
df myfunction: cwls hello%e.
ef myfunction.
```
#### esfd
- Execute source file dsm
- Executes dsm code in a file with a specified path.
`esfd helloworld.`
- The file can't have an extension since dsm separates statements with "."
