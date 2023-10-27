# 고기서 쿠킹 :meat_on_bone:

![GameIntro](https://github.com/YS-AN/UnityMeatCooking/assets/11987122/89552f90-004c-4282-b521-a589d41c6008)

내가 레스토랑 사장님?!?! <br>
음식 재료 주문부터, 가게 확장까지 나만의 레스토랑을 완성해보세요. 

---

## 개요
* 프로젝트 이름 : MeatCooking
* 장르 : 경영 시뮬레이션
* 개발 언어 : `Unity` `c#`

## 게임 설명
기술 문서 PPT : [고기서쿠킹 기술문서](https://docs.google.com/presentation/d/1CMZnPcSpElnzdk_fU3l9hI2PxtcI3Wwi/edit?usp=sharing&ouid=117600408879502028307&rtpof=true&sd=true)

### 핵심 로직 구현 
플레이어 상호작용 로직 
타이쿤 게임 특성상 플레이어와 상호작용 하는 오브젝트가 늘어날 수 있음.<br/>
따라서 플레이어와 상호작용하는 오브젝트(ex.가구, 손님)는 **커맨드 패턴**을 활용해 구현함. 

* 플레이어 (호출자)
https://github.com/YS-AN/UnityMeatCooking/blob/630f5e5b23d2126499c16a3f6771686c084a5f4d/Assets/Scripts/Chef/ChefMover.cs#L129-L156

* 수행할 행위 (Command)
https://github.com/YS-AN/UnityMeatCooking/blob/630f5e5b23d2126499c16a3f6771686c084a5f4d/Assets/Scripts/IActionable.cs#L5-L12

* 상호작용할 객체 (송신자)
카운터 : 플레이어가 접근하면 레시피가 나옴
https://github.com/YS-AN/UnityMeatCooking/blob/630f5e5b23d2126499c16a3f6771686c084a5f4d/Assets/Scripts/Food/RecipeAction.cs#L22-L34


## 시연 영상
:clapper: [고기서 쿠킹 시연영상](https://youtu.be/H3BjpUJZmK0?si=dH6cQ2XP1cynpiAn)
 
---

### 사용 Asset 자료 출처
* 레스트랑 가구 및 캐릭터 : "Restaurant Pack" by [Fubu Studios](https://assetstore.unity.com/packages/3d/environments/restaurant-pack-199227)
* 캐릭터 애니메이션 : [Mixamo](https://www.mixamo.com/)
* 배경음악 : "The Movement of India" by [Aakash Gandhi](https://studio.youtube.com/channel/UCON429aKW8PFIKTRj52XeJA/music)
