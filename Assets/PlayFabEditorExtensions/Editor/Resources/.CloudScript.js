handlers.updateLeaderboardScore = function(args, context) {
    var playFabId = args.playFabId; // The PlayFab ID of the user to update
    var newScore = args.newScore; // The new score to set

    var updateRequest = {
        PlayFabId: playFabId,
        Statistics: [{
            StatisticName: "PlayerIDLeaderboard",
            Value: newScore
        }]
    };
    var updateResult = server.UpdatePlayerStatistics(updateRequest);
    return updateResult;
};


handlers.removeLeaderboardEntry = function(args, context) {
    var playerId = args.playerId; // Player ID you want to remove
    var leaderboardId = args.leaderboardId; // The leaderboard you are targeting

    // Ensure the player ID is provided
    if (!playerId || !leaderboardId) {
        return { error: "Player ID and leaderboard ID must be provided." };
    }

    // Prepare the update for the player's statistic
    var request = {
        PlayFabId: playerId,
        Statistics: [{
            StatisticName: leaderboardId,
            Value: 0 // Setting to 0 to remove the entry
        }]
    };

    // Call the PlayFab API to update the player's statistic
    var updateResult = server.UpdatePlayerStatistics(request);
    
    return { success: true, message: "Leaderboard entry removed successfully." };
};
