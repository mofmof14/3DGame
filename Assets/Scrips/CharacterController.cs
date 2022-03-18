using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Game
{
    public class CharacterController : StateMachineBase<CharacterController>
    {
        public Animator MyAnimator;
        public float sp;
        private Slider spslider;
        private ThirdPersonController thirdPersonController;
        private StarterAssetsInputs _input;
        // Start is called before the first frame update
        void Start()
        {
            spslider = GameObject.Find("Spslider").GetComponent<Slider>();
            spslider.maxValue = sp;
            spslider.value = sp;
            thirdPersonController = GetComponent<ThirdPersonController>();
            _input = GetComponent<StarterAssetsInputs>();
            ChangeState(new CharacterController.Neutral(this));
        }

        // Update is called once per frame
        void Update2()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                
                if(sp<1){
                    return;
                }
                else
                {
                    MyAnimator.Play("attack");
                    sp -= 1;
                    spslider.value = sp;
                }   
            }
        }

        private class Neutral : StateBase<CharacterController>
        {
            public Neutral(CharacterController _machine) : base(_machine)
            {
            }
            public override void OnEnterState()
            {
                Debug.Log("Neutral OnEnterState");
                machine.thirdPersonController.canmove = false;
            }
            public override void OnUpdate()
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {

                    if (machine.sp > 0)
                    {
                        machine.sp -= 1;
                        //machine.MyAnimator.Play("attack");
                        machine.spslider.value = machine.sp;
                        machine.ChangeState(new CharacterController.Attack(machine));
                    }
                }
                else if (machine._input.move != Vector2.zero)
                {
                    machine.ChangeState(new CharacterController.Move(machine));
                }
            }
        }

        private class Move : StateBase<CharacterController>
        {
            public Move(CharacterController _machine) : base(_machine)
            {
            }
            public override void OnEnterState()
            {
                machine.thirdPersonController.canmove = true;
            }
            public override void OnUpdate()
            {
                if (machine._input.move == Vector2.zero)
                {
                    machine.ChangeState(new CharacterController.Neutral(machine));
                }
            }
        }

        private class Attack : StateBase<CharacterController>
        {
            private float Timer = 0f;
            public Attack(CharacterController _machine) : base(_machine)
            {
            }
            public override void OnEnterState()
            {
                machine.MyAnimator.Play("attack");
            }
            public override void OnUpdate()
            {
                Timer += Time.deltaTime;
                if (2f < Timer)
                {
                    machine.ChangeState(new CharacterController.Neutral(machine));
                }
            }
        }
    }
}