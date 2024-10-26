VAR BossWatching = false
VAR BossSuspicionCounter = 0
EXTERNAL runTask


-> start // this tells ink where to start the story

=== start ===
Hey buddy I've got a problem. I need you to fix it.
* [What's your problem, sir?]
-> ExplainProblem
* [I need your help.]
when BossWatching = true 
     BossSuspicionCounter++
-> NeedHelp

=== NeedHelp ===
What are you talking about? I'm the one who needs help. I have a problem and you must fix it. Are you going to do your job or not?
* [Yes, sir.]
-> ExplainProblem
* [No.]
-> GunnerAngry

=== ExplainProblem ===
I've lost my laptop. I have some important assets that I need you to recover.
* [...]
...
* [What needs to be recovered?]
Important things. Assets. I don't have to tell you that.
	** [...] 
- <> Fine. It's Bitcoin, I need you to recover it. It was very expensive. I spent a lot of money. 

* [How did you lose your laptop?]
* [Do you have an account you would be able to sign in to?]


-> END // this marks the end of the story