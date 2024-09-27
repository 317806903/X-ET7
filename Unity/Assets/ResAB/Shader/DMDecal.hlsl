#ifndef DM_DECAL_FUNCTION
#define DM_DECAL_FUNCTION


void GetAlpha_float(float3 WorldPos, float PosY, float Up, float UpGradient, float Down, float DownGradient, out float Alpha)
{
    if(WorldPos.y <= PosY + Up && WorldPos.y >= PosY - Down)
    {
        Alpha = 1;
    }else if(WorldPos.y > PosY + Up && WorldPos.y < PosY+Up+UpGradient)
    {
        Alpha = 1.0 - ((WorldPos.y - (PosY+Up))/UpGradient);
    }else if(WorldPos.y < PosY - Down && WorldPos.y > PosY-Down-DownGradient){
        Alpha = (WorldPos.y-(PosY-(Down+DownGradient)))/DownGradient;
    }else{
        Alpha = 0;
    }
    
}

#endif