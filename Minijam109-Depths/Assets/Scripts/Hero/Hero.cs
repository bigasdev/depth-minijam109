using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] float moveSpeed, jumpForceMultiplier, jumpBoostVanish = .2f;
    [SerializeField] float boostMax = 250, boostMultiplier = .03f, boostMin = 75;
    [SerializeField] BoxCollider2D colliderBox;
    float currentJumpForce, originalBoostVanish;
    bool triedToJump;
    private void Start() {
        originalBoostVanish = jumpBoostVanish;
    }
    private void Update() {
        OnHud();
        if(!GameInputManager.GetKeyDown("Interaction")){
            OnJump();
        }
    }
    private void FixedUpdate() {
        var movement = new Vector2(GameInputManager.GetAxisDown("Horizontal") * moveSpeed, 0);
        OnMove(movement);
        OnBoost();
        OnFall();
    }
    void OnHud(){
        Gamehud.Instance.SetBoostText((int)currentJumpForce);
    }
    void OnBoost(){
        if(currentJumpForce <= boostMax){
            if(GameInputManager.GetKeyDown("Interaction")){
                CameraManager.Instance.boostingShake(currentJumpForce);
                currentJumpForce += 1 * jumpForceMultiplier;
                triedToJump = true;
            }
            return;
        }
        currentJumpForce = 0;    
    }
    void OnJump(){
        if(currentJumpForce <= boostMin || !colliderBox.enabled)return;
        colliderBox.enabled = false;
        CameraManager.Instance.defaultShake();
        CameraManager.Instance.offset = new Vector2(0, 0);
        triedToJump = false;
    }
    void OnFall(){
        if(colliderBox.enabled)return;
        jumpBoostVanish += boostMultiplier;
        currentJumpForce -= 1 * jumpBoostVanish;
        if(currentJumpForce <= 0){
            CameraManager.Instance.offset = new Vector2(0, 3);
            currentJumpForce = 0;
            colliderBox.enabled = true;
            jumpBoostVanish = originalBoostVanish;
        }
    }
    void OnMove(Vector3 movement){
        this.transform.position += movement;
    }
}
