# UnityExample_Jupiter

# Plan
1. Hit Motion (△ : Projectile modify to normalized and Orge HitMotion(KnockBack x))
2. Destroy Effect (O)
3. More Character (X)
4. A* Algorithm Study (X)
5. UI Scene for Interact ( Very Small △ -> Small△ -> △ (Interactable Ok) )
6. Mobile X,Y Position Debug.. (Pc and Mobile Difference X,Y Position.. why?) (△) 
   => var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
7. make mobile screen appropriate ( Small△ => i don't know if it worked well or not )
8. Interact War Scene and Training Scene ( O )
9. War Scene in HealthBar Update ( O )
10. Mobile Touch Effect ( O )
11. Enhancement Effect Image ( Very Small △ -> Small △ )
12. Enhancement Chance Manipulation (ex) +1,+2,+3 100%, +4 51%, +5 34%, +6 26%, +7 16%...)
13. Enhancement successful enhancement, the gladiator's appearance changes.
14. Damage Effect (△ : TextMeshPro is bad.. but TextMesh is Not Bad )
15. KnockBack Effect
16. Enhanced resource management Class
17. Enhancement resource acquisition scene
18. Gladiator Make Scene? Class? UI?
19. Change the number of Gladiators to be applied in WarScene ( Very Small △ : Tracking System Bug.. ->  △ ScriptableObject bool applied)
20. Character Start() -> Gladiator Stat Initialize Setting Update
21. The number of Gladiator Enemy Collider2D Update.. this is Attack func
22. War Scene: Ai Vs Ai Battle Tracking System ( Very Small △ : MissingReferenceException: The object of type 'Transform' has been destroyed but you are still trying to access it.
Your script should either check if it is null or you should not destroy the object.)
23. Many Ai Case ? ( A Site , B Site of Ai Transform tracking how does behave? )
24. How to write A, B Site damage judgment? ( => Solved using LayerMask? )
25. LayerMask is bitmask..( ex) 11, 12 LayerMask Convert to 6144(DEC) -> 0001 1000 0000 0000 (32-bit) ) => (△ I don't know if this solution ok..)
26. AttackSpeed applied and AttackSpeed applied Damage count measurement  ( ( OK ) )
27. Converting War Scene to Training Scene ( O )
28. Adding Win/Lose Animation to War Scene ( O )
29. Skill Attack Adding...( Very Small △ )
30. Skill Tree UI Make ( Very Small △ )
31. Skill Trainer UI Make ( Very Small △ )
32. Rewriting the Gladiator class using DontDestroyOnLoad ( O: Training Room -> War Room -> Training Room ) 
33. Skill Action Text UI Make ( 14 reference )
34. OrgeDamageLayer Debug.Log Check: if attack 1 time, Debug.Log 1 time ?

# Bug
1. A* Enemy Attack Motion Not Smooth..
2. Mobile X,Y Position Debug.. (Pc and Mobile Difference X,Y Position.. why?) => (Plan 6,7 reference)
3. Orge Right Attack Motion Check...
4. After switching scenes from TrainingRoom Scene to War Scene, there is a disconnection phenomenon in the scene where damage is received after the first attack
5. Once Attack Motion but Twice hit Damage

# Temp Play Video
https://user-images.githubusercontent.com/27074717/129921769-ddc908ad-1033-4c31-8a04-7a5ea060c6d2.mp4






