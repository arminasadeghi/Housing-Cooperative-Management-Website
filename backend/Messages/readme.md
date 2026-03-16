# Events & EventHandlers


all the events that send or receive via the rabbit mq (which called domain events)  go here


## Event 

A command is defined as a method that notify other microservices as Async . 

Events are statements of fact . 

Each event contains data which want to transfer .

Events are things that have happended within a system .

They are the result (not in mehtod return sense ) of an executed command .

## EventHandler 

EventHandler is implemented by microservices which want to receive message from event sender.

EventHandler is space of implementation of method's logic . 

# Implementation


Events are named in the past tense

A event and its Handler should be implement in one file 

event sender : just implement event

event subscriber : implement both event and eventHandler

## NamingPattern   


	VerbHappend 

	OrderPlaced

	InvoiceGenerated

	DiscountApplied

