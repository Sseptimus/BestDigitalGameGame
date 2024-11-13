INCLUDE globals.ink
EXTERNAL runTask(taskType)
EXTERNAL bossSpotted(bossSeen)
VAR bTaskFail = false
VAR bTaskSuccess = false

-> Start

=== Start ===

Hello? Is this okay?
 
 * Hello and welcome to ASOS[] How can I help you today?
 * Is what okay?[]
 My voice I mean. 

- Can you hear me okay?

* Yes[], I can hear you fine.

Okay good. I was told it would be hard to understand true intentions today. Thought it might be regarding the phone. Anyhoo, its Me.

	** ...Me?[] Ma'am, I'm sorry but I'll need your name to assist you today.

	Oh! I'm so sorry! I thought you were the other gentleman. Hello my new friend! My name is Chamomile. Like the tea.

		*** Hi Chamomile.[] How can I help you today?

		Why, I can't log in to my prayer chain. I have to make sure my followers know their futures today. What ever shall they do with the uncertainty?? 

			**** Well Ma'am, the first step[] is to test the validity of your account.

			I'm sorry, but I dont know what that means.

				***** Okay, so[] Im going to send you a short list of your security questions. Please fill them out while I look at your details.

				//start the game
				~ temp gameType = "chimps"
				~ runTask(gameType)
				-> END

=== TaskFailed ===
{ 
- TURNS_SINCE(-> FirstFail) >= 0:
	Oh my, today really isn't my day it seems. I am just not getting these. You know, is your day going just as bad as mine?
	-> AskQuestions
- else:
	-> FirstFail
}
-> END


=== FirstFail ===
It doesn't seem to be working. Am i doing these wrong? I swear it is MY account!
~ temp gameType = "chimps"
~ runTask(gameType)
-> END


=== TaskSuccess ===
~ bTaskSuccess = true

//then if you succeed - start with Chamomile
I think thats all of them. Now, since we've just met, tell me about yourself. Hows your day going?
-> AskQuestions

=== AskQuestions ===
*It's...[]Been quite the day. I am chained to my chair, and there is some shadow creature making sure I talk to you!
-> AnswerQuestion

 *I don't think I should discuss[] my day with you.
-> AvoidQuestion



//answer somthing about your actual day
=== AnswerQuestion ===
 
 //discuss day response
 Oh my, thats quite the response! Keep going! What else?
~ HelpCounter += 1
 
 * I...[] okay, well I'd really like to get out of here! Please!
 
 - Ha ha! There is so much energy there! So much gusto! My, thats perfect! Okay, and what can you do to change that? How can we help you to help yourself?
 
 * Help myself...
 * Are you listening[?] to me?
 
 - You have so much energy for your goals, and thats beautiful. Now you need to realise for yourself how you can bring ACTION to the words you are saying in your own way.
 
 *My own way...?
 *You aren't even hearing me[]...
 
 - Nobody can give you the power to change your life. Only you can do that. You have to decide for yourself that you want change, and take it!
 
 *Ma'am, I'm sorry[], I think I made a mistake.
 
 - No no! You have begun the journey of healing.
 
 * Journey of...[] I just said I was... I am extremely confused ma'am.
 
 - Confused? That means you are doing well! But maybe you are right. Am I rushing you?
 
 * Personally[...], I feel you are pushing me.
 
 - But if you aren't pushed, would you ever make progress?
 
 * I...
 
 - Perhaps some more encouragement? some proof of change being possible?
 
 * I mean...
 
 - A reading!
 
 * What?
 -> TarotReadingQuestion

 
 === AvoidQuestion ===

 //dont discuss day response
 Oh... Oh well, okay then. May I tell you about my day?
 
 * We can't hang up[] before you do, ma'am.
 
 - Okay well, thats something at least. So, I wake up right, and imagine my suprise when I cant start my morning forcast. So I check the cards, and they say there is a "Data Leak" and my "Account" cant be "Used". As you can imagine, I was very frustrated.
 
 * That sounds[] very frustrating ma'am.
 
 - Indeed it was! And so I of course had to ask the cards. You know what I mean. Straighten things out a bit in my head... You DO know what I mean right?
 
 * Ask the cards[?]...? I... I mean not really. I'm sorry.
 -> TarotReadingQuestion
 

 === TarotReadingQuestion ===

 Oh! Well, I of course mean the Tarot. Normally I use a Thelema selection, but I don't imagine that would be pertinant to assume the use of here. a Smith-Waite Major Arcana Deck is ideal for introductions.
 
 *[Over the phone?] And you wish to do this over the phone Ma'am?
 
 - Of course! Then it can guide you forward. Now, I'm just shuffling the deck here. Then I'm going to do a short draw of three cards, okay? 
 
 And then the cards I draw will have meaning, depending on the card and the order. One for morning, the past, one for midday, the present, and one for evening, the future. Does that make sense?
 
 * I mean in theory[], but...
 
 - Don't worry. It'll all make sense in just a minute.
 
 * I'd rather we just stopped[] all this Ma'am. Please.
 Well... I can't say I'm not dissapointed. You were making such good progress. But I of course will respect your intentions and your privacy. I hope you have a long think about what you want.
 	
	**I will. Thank you[]...
 
 	Call me Chamomile.
 
 		*** Thank you[] Chamomile.
 
 		It was a pleasure.
 
 		-> END
 
 
 
 *Okay. []I trust you.
-> TarotReading

=== TarotReading ===
 ~ bossSpotted("seen")
 ~ BossSuspicionCounter += 2
 
 Well thats rather sweet. Okay. I am done shuffling. So I am going to draw three cards for you from this deck I have here. 
 
 This first card will be signifying your past. Something to speak towards where you have come from. Trials that have passed over you. Make sense?
 
 * I think so.
 
 - Good, and ...Oh my...
 
 * Oh my? What?
 
 - The Tower. Something signifying Distress and Adversity. A Large change. It can be good, but not as a flat first draw. You have seen misery leading you to here. Pain and suffering? Something looming over you?
 
 * I mean[...], that...
 
 - Oh my, that isnt a good start is it. Lets move on. Its got to get better... 

 Oh okay. So here is the High priestess. Now she is interesting, especially in your present. 
 
 She can mean many things, not as binary as the Tower. She is full of mystery, and secretive. 
 
 You are surrounded by confusion? Things unknown to you, yet known to others. You are unknown, even to yourself...
 
 * Well[...] that doesn't sound better...
 
 You are full of sadness and mystery my friend. What an interesting read. Wwo of the major arcana as well. 
 Hmm...
 
 * What?[] What does that mean?
 
 - Huh? Oh I'm sorry dear. Lets see your third one then... Oooh swords...
 
 *A sword?[]?? I can't use a sword...
 
 - Oh no! Hahah Not a real sword no. The Seven of Swords. A card from the deck again.
 
 * I.. Oh...[] Heh sorry... I feel a little silly now.
 
 - Don't beat your self up. It's hard without being able to see the cards. The art is rather pretty on these ones too. A shame...
 
 * Ah[], so the seven of swords?
 
 - Oh Yes of course! So, the seven of swords is a card about uncertainty and planning. In your future is a plan or design that has a strong maybe in it. 
 
 Something that may succeed. It hangs in the balance, easily swayed with pushes either way. 
 
 It speaks of attemping things, with hope and confidance, but also of quarrelling, arguing, and annoyance. 
 It's a future of perhaps.
~ HelpCounter += 1
 
 * A future of perhaps?[] Hmm... Thats incredibly cryptic.
 
 - Well, thats quite the future you have there.
 
 * I mean...[] Thank you? I guess?
 
 - Oh, its not a compliment. It sounds like you have a lot going on. In your head and your heart. Where did you say you worked again?
 
 * [I work for OSOS] Well, I work for OSOS obviously. The local branch, I believe.
 
 - Great. I have written that down. Perhaps I'll see you in person some day?
 
 * I... what?
{ 
  - bTaskSuccess == true:
	-> SuccessGameEnding
  - else:
	-> FailGameEnding
}

->END


 
 === SuccessGameEnding ===
 Well, I have to go. I've missed my first stream, but the second one is just as important. I'm finding the latent energies of the internet and calming them for the masses. Thank you again for the help.
 
 * Thank you Chamomile.
 
 Bye!

~ HelpCounter += 4

 -> END
 

 === FailGameEnding ===
 Well, I have to go pick flowers. If I make a tincture of lambs breath and foxglove, and bathe my modem, maybe my account will fix itself. Thank you for the lovely chat, and for trying to help.

* I hope that works.

Heh, it worked last time. Be well!

~ HelpCounter += 2

-> END






