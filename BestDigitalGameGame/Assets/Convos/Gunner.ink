VAR BossInView = false


-> start // this tells ink where to start the story

=== start ===
This is LIVE how can I help you today?
->when BossInView == false->NeedHelpNoBoss
->when BossInView == true ->NeedHelpBoss

=== NeedHelpBoss ===
Hey buddy I need your help
*** What can I help you with sir?
-> LongWindedExplanation

=== NeedHelpNoBoss ===
Hey buddy I need your help
*** What can I help you with sir?
-> LongWindedExplanation
*** No I need your help
-> GunnerConfused

=== LongWindedExplanation ===
Hey buddy I don't need your attitude
I just lost 200 million in bitcoin and you're going to find it for me
***What how am i supposed to...
-> HowAmI

=== HowAmI ===
What how am i supposed to...    
-> RandomStuffIDidntFinishDontJudgeMe

=== RandomStuffIDidntFinishDontJudgeMe ===
I got home yesterday blah blah blah
-> END

=== GunnerConfused ===
Huh? I'm the one that needs help not you
-> END // this marks the end of the story