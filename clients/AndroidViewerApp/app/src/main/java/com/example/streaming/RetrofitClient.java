package com.example.streaming;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public final class RetrofitClient {
    private static PlaylistService instance;

    private RetrofitClient() {
    }

    public static PlaylistService getInstance() {
        if (instance == null) {
            Retrofit retrofit = new Retrofit.Builder()
                    .baseUrl("http://10.0.2.2:5100/")
                    .addConverterFactory(GsonConverterFactory.create())
                    .build();
            instance = retrofit.create(PlaylistService.class);
        }
        return instance;
    }
}
