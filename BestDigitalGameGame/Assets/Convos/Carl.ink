INCLUDE globals.ink
EXTERNAL runTask(taskType)
EXTERNAL bossSpotted(bossSeen)
VAR bTaskFail = false
VAR bTaskSuccess = false

-> start // start story here

=== start ===
Hello, Carl here. I need some help with the police station computer.
* What's the issue?
* Can you help me[]?
Sorry, help you? This is OSOS, correct?
	** Yes, this is OSOS[]. I need your help. I need to get out of here.
	{ BeingWatched == true:
		~ bossSpotted("seen")
		~ BossSuspicionCounter +=1
	}
	Sorry, am I being pranked? I can't deal with this right now.	
	*** No[] I'm not pranking you. How can I help you out?

- My work computer keeps shutting off. The screen turns blue and I can no longer use it. 

This has happened 3 times, about 11:45am the first time, 12:07pm the second time, and again just 5 minutes ago.

It's highly important that I recover use of this computer shortly. 
I have a lot of important case files to go through.

* [Virus?] Have you visited any... questionable sites recently? You might have a virus.
Of course not! How could you suggest such a thing?
	** You never know[]. Is there any other way you might have a virus?
* [Different computer?] Have you tried using a different computer?
Of course I've tried using a different computer. That's what I'm using currently. However, I need my normal computer to get my work done effectively. 

- I did get some strange popups and screen glitches not long before the first crash. That could perhaps have been linked? I can't draw any definitive conclusions. 

* Definitely sounds like malware[]. 
Perhaps. Could you investigate? I am in quite a rush.

* Please, I'm not a prank caller[]. Please help me. I need your help. 
{ BeingWatched == true:
	~ bossSpotted("seen")
	~ BossSuspicionCounter +=1
}
What are you talking about? Is everything alright?
	** I can't explain[]. I'm trapped. I need you to get me out. I can't work here any longer.         // better answer for more help points
	~ HelpCounter += 2
	{ BeingWatched == true:
		~ bossSpotted("seen")
		~ BossSuspicionCounter +=1
	}
	Trapped? Let me see what I can do. But first I need my data recovered.

	** No, I need to get out of here[]. Get me out. Get me out of here.      // Gets smaller amount of help points 
	~ HelpCounter +=1
	 I don't understand... I'll need more time to investigate. But first, I need you to fix my computer. Maybe then I can help you.

- This computer is crucial to me being able to work effectively.
* I'll try[] my best.

~ temp gameType = "minesweeper"
~ runTask(gameType)
-> END

=== TaskFailed ===
{ 
- TURNS_SINCE(-> FirstFail) >= 0:
	I'm sorry, that's twice you haven't fixed it now... I have to call someone else. I don't have time for this.
	-> END
- else:
	-> FirstFail
}
-> END

=== FirstFail ===
Are you kidding me? Gosh, how am I supposed to help anyone if you can't even recover my computer?
* I'm sorry.
I'm sorry for getting upset. I'm very stressed with all this policework. I'm going to call someone else now. I hope you get the help you need. 
-> END

* Sir, let me try again[], I need your help.
Alright, but I don't have much time. 
	~ temp gameType = "chimps"
	~ runTask(gameType)
	->END



=== TaskSuccess ===
Oh thank you so much! That was intense, I could see it from the other side of the office! It seems to be logging in okay now.
* Before you go...
Yes?
	** Please, please help me.[] I need to get out of here. I can't do this any longer.
	{ BeingWatched == true:
		~ bossSpotted("seen")
		~ BossSuspicionCounter +=1
	}
	Can you expand at all? I don't have enough details to make a case.
		*** They're keeping me here.[] They're watching me. I can't say too much.
		Who is?
			**** Corporate[], listen, if you call again, ask for \[REDACTED\]. 
			I'll do my best. Thank you for your help. Carl, over and out.
			{ BeingWatched == true:
				~ bossSpotted("seen")
				~ BossSuspicionCounter +=1
			}
			~ HelpCounter += 3
			-> END
* That's no problem.[] Give me a call if you have any more issues.
Cheers.
-> END
