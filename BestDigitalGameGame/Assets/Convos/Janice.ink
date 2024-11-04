INCLUDE globals.ink
EXTERNAL runTask(taskType)
VAR bTaskFail = false
VAR bTaskSuccess = false

-> start // this tells ink where to start the story

=== start ===
Oh, good morning, my name is Janice, and, and I've got a little bit of a problem with my emails. Can you help me by any chance?
* Of course I can help you ma'am
Oh thank you my dear, I do really appreciate it.
** [Ask for help] Ma'am, I need your help.
{ BeingWatched == true:
	~ BossSuspicionCounter +=1
}
What do you mean sorry dear? I'm not sure I understand. I need help with my emails.

** [Don't ask for help] Of course, it's no problem!

* What's your problem?
Oh, I'm sorry to bother you, it's just it's quite a big problem.

- I've been trying to fix this for hours you see, I just can't seem to figure it out. 

I keep getting these emails, with different things for me to buy, and I don't want to buy them. 

The emails just keep filling up and there's too many of them.

* Have you tried unsubscribing?
How do I do that?
** Log in to your email[] account and grant me access. I'll take it from there.

* [Can you log in to email?] I need you to log in to your email account and grant me access.

- Oh yes, I can log into my email account. Just give me one moment.
* Can you help me?
Help you with what, dear? I'm just trying to log in to my emails, won't be long dear.
{ BeingWatched == true:
	~ BossSuspicionCounter +=1
}

* Of course, that's no problem ma'am.

- Ah there we go, I've logged into my emails now, do you know how to help me?
* Yes[], I just need to complete this sequence.

* Please [help] I really need your help.
What do you mean, dear, is everything alright? 
** [They're watching me.] I can't say too much. They'll know.
I'm so sorry dear, I don't think I understand. 
How is my email coming along?
*** That's okay[], I just need to complete this sequence. I won't be long...

- Oh thank you so much my dear, I'll be so glad to get rid of these emails!
~ temp gameType = "chimps"
~ runTask(gameType)

-> END // this marks the end of the story

=== TaskFailed ===
Oh! That didn't look good.
Did you manage to complete it?
* No.[] I'm sorry. I failed the task. I won't be able to get rid of your spam emails.
That's okay dear. You tried your best, and that's all that matters.
~ BossSuspicionCounter += 1
-> END

=== TaskSuccess ===
Hi dear, how is it coming along? I don't want to rush you or anything, but are you doing alright?
* Yes[], everything should be fine now. You shouldn't get any more of those emails.
Oh thank you my dear, I really appreciate it. Well, I will let you know if I have any more problems. 

* No, everything is awful[].
Oh no, what's wrong dear? Are you struggling to complete it? If you can't do it that's okay, I can call someone else.
** It should be okay now.[] You shouldn't get any more emails. I just thought perhaps you cared to ask how I was. I need your help, I'm sorry, I really need help. 
Of course I care, dear. I don't know who you are but I think everyone is important. You keep asking for help, I'm not sure what you want, but I'll give you another call soon, okay? Everything will be fine, my dear.

- Thank you for all your help. I'll let you keep going with your work, thank you dear. Goodbye.
-> END