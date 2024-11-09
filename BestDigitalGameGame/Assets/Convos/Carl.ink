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
	** I can't explain[]. I'm trapped. I need you to get me out.          // better answer for more help points
	** No, I need to get out of here[]. Get me out. Get me out of here.      // Gets smaller amount of help points (TODO)
	 I don't understand. I'll need more time to investigate. But first, I need you to fix my computer. Maybe then I can help you.

- This computer is crucial to me being able to work effectively.
* I'll try[] my best.

~ temp gameType = "minesweeper"
~ runTask(gameType)
-> END

=== TaskFailed ===
-> END

=== TaskSuccess ===
-> END
