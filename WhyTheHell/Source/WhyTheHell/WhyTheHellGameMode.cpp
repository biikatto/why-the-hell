// Copyright 1998-2015 Epic Games, Inc. All Rights Reserved.

#include "WhyTheHell.h"
#include "WhyTheHellGameMode.h"
#include "WhyTheHellPawn.h"

AWhyTheHellGameMode::AWhyTheHellGameMode(const FObjectInitializer& ObjectInitializer) : Super(ObjectInitializer)
{
	// set default pawn class to our character class
	DefaultPawnClass = AWhyTheHellPawn::StaticClass();
}

