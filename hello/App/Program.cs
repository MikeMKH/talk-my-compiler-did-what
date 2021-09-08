using System;

var group = "Wisconsin .Net";
Console.WriteLine($"Hello {group} User Group!");

Action<string> sorry =
  x => Console.WriteLine(
    $"Sorry, {x} this is a bit ridiculous.");

sorry("everyone");
Closing("fun");

static void Closing(string state)
  => Console.WriteLine($"Hope you find it {state}!");
/*
Hello Wisconsin .Net User Group!
Sorry, everyone this is a bit ridiculous.
Hope you find it fun!
 */
