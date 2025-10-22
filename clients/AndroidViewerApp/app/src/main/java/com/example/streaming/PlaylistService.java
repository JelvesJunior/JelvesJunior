package com.example.streaming;

import com.example.streaming.models.Playlist;
import com.example.streaming.models.PlaylistItem;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Path;

public interface PlaylistService {
    @GET("api/playlists")
    Call<List<Playlist>> getPlaylists();

    @GET("api/playlists/{id}")
    Call<Playlist> getPlaylist(@Path("id") int id);

    @GET("api/playlists/{id}/items")
    Call<List<PlaylistItem>> getPlaylistItems(@Path("id") int id);
}
