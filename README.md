# GeneticTerrain
Exercice IN'TECH création algorithme génétique

The goal is to determine the best equation that describe a topography (from an image with grey scale) by generating the equation with an ast.

DotNetFramework 4.7.2
FluentAssertion
NUnit

## AST (Abstract Syntaxic Tree)

This library provide parsing, tokenization of the equation and generation of the AST nodes. It provide also the Visitors that can travel to the ASTs graphs.
They can print the graph to string, edit, mutate, compute the result of the parsed graph.
The visitor pattern allow to explore the graph easliy by passing himself as parameter to the abstract method Accept(), thus call the corresponding visit() method inside the visitor.

## Runner

Simple console UI runner to execute the simulation.

## GeneticTerrain

The core of the simulation.  Each calculus possibility is called an algorithm, a bestkeeper collection store the algorithm and keep the best algorithm.
See GeneticTerrain.cs:RunSimulation() for complete description of the process
It also log the results of the simulation.

## Tests

The unit tests... each function of the project should have been tested... (In theory)

## Vizualiser

Allow to print a bitmap of a specific algorithm and should allow to read a bitmap in the future.