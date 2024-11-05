INCLUDE globals.ink
EXTERNAL runTask(taskType)
VAR bTaskFail = false
VAR bTaskSuccess = false

-> start // this tells ink where to start the story

=== start ===
Hey buddy I've got a problem. I need you to fix it.
* What's your problem, sir?
-> ExplainProblem
* I need your help.
-> NeedHelp

=== NeedHelp ===
What are you talking about? I'm the one who needs help. I have a problem and you must fix it. Are you going to do your job or not?
* Yes, sir.
-> ExplainProblem
* No.
-> GunnerAngry

=== ExplainProblem ===
I've lost my laptop. I have some important assets that I need you to recover.
* ...
...
* What needs to be recovered?
Important things. Assets. I don't have to tell you that.
	** ...
- Fine. It's Bitcoin, I need you to recover it. It was very expensive. I spent a lot of money. 

* How did you lose your laptop?
That's irrelevent. Just get me my bitcoin back. 
* Do you have an account[] you would be able to sign in to?
Yes, I have an OSOS account.

- I need you to sign into my account for me. I can't complete the login sequence.
~ temp gameType = "numberPuzzle"
~ runTask(gameType)
{ gameType == "numberPuzzle":
	->TaskDialogueNumberPuzzle
}
-> END

=== TaskDialogueNumberPuzzle ===
Oh, a sliding puzzle. You should be able to complete this easily. Just click on the square you want to move.
-> random_comment

= random_comment
	{~ Move that square left. | Move that square down. | Move number 5 to the right. | Slide that one up. | You should listen to me, I know how to do this. }
->END


=== GunnerAngry ===
Why aren't you going to help me? That's your job. Do your job or I will report you.
 * Please help me[]. I have to escape my job. I can't do this any longer. 
Well, I'll certainly help you! Get fired! You're supposed to fix my problem, not beg to me!
~BossSuspicionCounter += 1
-> END
* Fine, I'll help you.
Great, see, that wasn't so hard. 
-> ExplainProblem

=== TaskFailed ===
You've taken too long! I cannot wait any longer, I have important matters to attend to. I will get someone more competent to fix this for me. 
~ BossSuspicionCounter += 1
-> END 

=== TaskSuccess ===
Well... thanks. I've just been given access. I can recover my Bitcoin. But I want a new laptop too. A good customer like me deserves it replaced.
* I can't [do that] give you a new laptop.
Why not? I've been a loyal customer and you should give me a new one. I asked nicely.
** Unfortunately[...], sir, I can't do that. I have recovered your Bitcoin for you. Have a good day, sir.
Fine. Thanks for your help.
-> END