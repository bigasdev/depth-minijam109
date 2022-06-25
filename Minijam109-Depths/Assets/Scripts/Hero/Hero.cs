using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private static Hero instance;
    public static Hero Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Hero>();
            return instance;
        }
    }
    [SerializeField] float moveSpeed, jumpForceMultiplier, jumpBoostVanish = .2f;
    [SerializeField] float boostMax = 250, boostMultiplier = .03f, boostMin = 75;
    [SerializeField] BoxCollider2D colliderBox;
    [SerializeField] SpriteSquash spriteSquash;
    [SerializeField] GameObject spriteRenderer;
    float currentJumpForce, originalBoostVanish;
    bool triedToJump;
    public bool canCharge;
    public bool isFalling{
        get{
            if(colliderBox.enabled)
            return false;
            else return true;
        }
    }
    private void Start() {
        originalBoostVanish = jumpBoostVanish;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        spriteSquash.Squash(.65f, 1);
    }
    private void Update() {
        OnHud();
        if(GameInputManager.GetKeyPress("Dash")){
            spriteSquash.Squash(.65f, 1);
        }
        if(!GameInputManager.GetKeyDown("Interaction")){
            OnJump();
        }
        if(GameInputManager.GetAxisPress("Horizontal") == 1){
            spriteSquash.Squash(.65f, 1.2f);
            spriteRenderer.transform.localScale = new Vector2(1,1);
        }
        if(GameInputManager.GetAxisPress("Horizontal") == -1){
            spriteSquash.Squash(.65f, 1.2f);
            spriteRenderer.transform.localScale = new Vector2(-1,1);
        }
        if(GameInputManager.GetKeyPress("Interaction") && !canCharge){
            CameraManager.Instance.boostingShake(200f);
            TalkingHudManager.Instance.InitializeHud(this.transform, "I need to get in the tube to be able to jump");
        }
    }
    private void FixedUpdate() {
        var movement = new Vector2(GameInputManager.GetAxisDown("Horizontal") * moveSpeed, 0);
        OnMove(movement);
        if(canCharge)OnBoost();
        OnFall();
    }
    void OnHud(){
        Gamehud.Instance.SetBoostText((int)currentJumpForce);
    }
    void OnBoost(){
        if(currentJumpForce <= boostMax){
            if(GameInputManager.GetKeyDown("Interaction")){
                //spriteSquash.Squash(1-(1/currentJumpForce), 1);
                CameraManager.Instance.boostingShake(currentJumpForce);
                currentJumpForce += 1 * jumpForceMultiplier;
                triedToJump = true;
            }
            return;
        }
        currentJumpForce = 0;    
    }
    void OnJump(){
        if(currentJumpForce <= boostMin || isFalling)return;
        spriteSquash.Squash(.65f, 1);
        colliderBox.enabled = false;
        CameraManager.Instance.defaultShake();
        CameraManager.Instance.offset = new Vector2(0, 0);
        triedToJump = false;
    }
    void OnFall(){
        if(!isFalling)return;
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
    public void SetChargeState(bool state){
        if(state == canCharge || isFalling)return;
        canCharge = state;
        if(state!=true){
            TalkingHudManager.Instance.InitializeHud(this.transform, "Leaving the tube!");
        }
    }
}
