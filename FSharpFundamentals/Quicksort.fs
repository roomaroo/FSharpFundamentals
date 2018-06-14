﻿module Quicksort

let rec quicksort = function
    | [] -> []
    | x::xs -> 
        let smaller = List.filter ((>) x) xs
        let larger = List.filter ((<=) x) xs
        quicksort smaller @ [x] @ quicksort larger

let sorted = quicksort [4;5;4;7;9;1;6;0;-99;1000;3;2]