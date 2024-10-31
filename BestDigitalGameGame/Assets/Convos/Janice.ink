INCLUDE globals.ink
EXTERNAL runTask(taskType)

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